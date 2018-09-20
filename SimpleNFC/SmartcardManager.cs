using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleNFC
{

    internal enum SmardcardState
    {
        None = 0,
        Inserted = 1,
        Ejected
    }

    class SmartcardManager : IDisposable
    {

        private static readonly SmartcardManager _instance = new SmartcardManager();

        private int _context;
        private bool _contextState = false;
        private int _lastErrorCode;
        private bool _disposed = false;
        private Card.SCARD_READERSTATE _state;
        private int _readerCount;
        private BackgroundWorker _worker;

        private SmartcardManager()
        {
            this._context = 0;
            this.EstablishContext();
            ArrayList availableReaders = this.ListReaders();
            this._readerCount = availableReaders.Count;
            this._state = new Card.SCARD_READERSTATE();
            this._state.RdrName = availableReaders[0].ToString();

            if (availableReaders.Count > 0)
            {
                this._worker = new BackgroundWorker();
                this._worker.WorkerSupportsCancellation = true;
                this._worker.DoWork += WaitChangeStatus;
                this._worker.RunWorkerAsync();
            }
        }

        public static SmartcardManager GetManager()
        {
            return _instance;
        }

        private void WaitChangeStatus(object sender, DoWorkEventArgs e)
        {
            while (!e.Cancel)
            {
                int result;

                lock (this)
                {
                    if (!this._contextState)
                    {
                        return;
                    }

                    result = Card.SCardGetStatusChange(this._context, 1000, ref this._state, this._readerCount);
                    //Console.WriteLine(this._state.RdrEventState);
                    //Console.WriteLine(Card.SCARD_STATE_CHANGED);

                    string cardUID = "";
                    byte[] receivedUID = new byte[256];
                    Card.SCARD_IO_REQUEST request = new Card.SCARD_IO_REQUEST();
                    request.dwProtocol = Card.SCARD_PROTOCOL_T1;
                    request.cbPciLength = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Card.SCARD_IO_REQUEST));
                    byte[] sendBytes = new byte[] { 0xFF, 0xCA, 0x00, 0x00, 0x00 }; //get UID command for Mifare cards
                    int outBytes = receivedUID.Length;
                    int status = Card.SCardTransmit(this._context, ref request, ref sendBytes[0], sendBytes.Length, ref request, ref receivedUID[0], ref outBytes);

                    if (status != Card.SCARD_S_SUCCESS)
                    {
                        cardUID = "Error";
                    }
                    else
                    {
                        cardUID = BitConverter.ToString(receivedUID.Take(4).ToArray()).Replace("-", string.Empty).ToLower();
                    }

                    Console.WriteLine(cardUID);
                }

                if (result == Card.SCARD_E_TIMEOUT)
                {
                    continue;
                }

                if ((this._state.RdrEventState & Card.SCARD_STATE_CHANGED) == Card.SCARD_STATE_CHANGED)
                {
                    int state = 0;
                    if ((this._state.RdrEventState & Card.SCARD_STATE_PRESENT) == Card.SCARD_STATE_PRESENT 
                        && (this._state.RdrEventState & Card.SCARD_STATE_PRESENT) != Card.SCARD_STATE_PRESENT)
                    {
                        state = 1;
                    }
                    else if ((this._state.RdrEventState & Card.SCARD_STATE_EMPTY) == Card.SCARD_STATE_EMPTY
                        && (this._state.RdrEventState & Card.SCARD_STATE_EMPTY) != Card.SCARD_STATE_EMPTY)
                    {
                        state = 2;
                    }
                    if (state != 0 && this._state.RdrCurrState != 0)
                    {
                        switch (state)
                        {
                            case 1:
                                {
                                    MessageBox.Show("Card inserted");
                                    break;
                                }
                            case 2:
                                {
                                    MessageBox.Show("Card ejected");
                                    break;
                                }
                            default:
                                {
                                    MessageBox.Show("Some other state...");
                                    break;
                                }
                        }
                    }
                    this._state.RdrCurrState = this._state.RdrEventState;
                }
            }
        }

        private bool EstablishContext()
        {
            this._lastErrorCode = Card.SCardEstablishContext(Card.SCARD_SCOPE_SYSTEM, 0, 0, ref this._context);
            Console.WriteLine("ERR: " + this._lastErrorCode);
            if (this._lastErrorCode != Card.SCARD_S_SUCCESS)
            {
                this._contextState = false;
                return false;
            }
            else
            {
                this._contextState = true;
                return true;
            }
        }

        private ArrayList ListReaders()
        {
            ArrayList result = new ArrayList();
            if (this.EstablishContext())
            {
                int size = this.GetReaderListBufferSize();
                byte[] readerList = new byte[size];
                this._lastErrorCode = Card.SCardListReaders(this._context, null, readerList, ref size);
                if (this._lastErrorCode == Card.SCARD_S_SUCCESS)
                {
                    string rName = "";
                    int indx = 0;
                    if (size > 0)
                    {
                        // Convert reader buffer to string
                        while (readerList[indx] != 0)
                        {

                            while (readerList[indx] != 0)
                            {
                                rName += (char)readerList[indx];
                                indx++;
                            }

                            //Add reader name to list
                            result.Add(rName);
                            rName = "";
                            indx++;

                        }
                    }
                }
                else
                {
                    MessageBox.Show(Card.GetScardErrMsg(this._lastErrorCode));
                }

            }
            return result;
        }

        private int GetReaderListBufferSize()
        {
            int result = 0;
            this._lastErrorCode = Card.SCardListReaders(this._context, null, null, ref result);
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).            
                }

                //Free your own state (unmanaged objects).
                //Set large fields to null.
                this._state = new Card.SCARD_READERSTATE();
                this._worker.CancelAsync();
                this._worker.Dispose();
                this._context = 0;
            }
            this._disposed = true;
        }
    }
}
