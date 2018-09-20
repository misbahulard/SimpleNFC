using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleNFC
{
    public partial class Form1 : Form
    {

        //private SmartcardManager manager = SmartcardManager.GetManager();

        //public Form1()
        //{
        //    InitializeComponent();
        //}

        int retCode;
        int hCard;
        int hContext;
        int Protocol;
        public bool connActive = false;
        string readername = "";
        string msg = "";
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, nBytesRet, reqType, Aprotocol, dwProtocol, cbPciLength;

        public Card.SCARD_READERSTATE RdrState;
        public Card.SCARD_IO_REQUEST pioSendRequest;

        DBConnect db = new DBConnect();

        public Form1()
        {
            InitializeComponent();
            SelectDevice();
            establishContext();
            connectCard();
            //CardState();
        }

        private void getUidBtn_Click(object sender, EventArgs e)
        {
            if (connectCard())
            {
                string cardUID = getcardUID();
                string msg = "UID: " + cardUID;
                printMessage(0, msg);
            }
        }

        private void numericReadSector_ValueChanged(object sender, EventArgs e)
        {
            if (numericReadSector.Value % 4 == 3)
            {
                readBlockBtn.Enabled = false;
            }
            else
            {
                readBlockBtn.Enabled = true;
            }
        }

        private void numericWriteSector_ValueChanged(object sender, EventArgs e)
        {
            if (numericWriteSector.Value % 4 == 3)
            {
                writeBtn.Enabled = false;
            }
            else
            {
                writeBtn.Enabled = true;
            }
        }

        private void turnOffBuzzerBtn_Click(object sender, EventArgs e)
        {
            string text = "";

            connectCard();

            ClearBuffers();
            SendBuff[0] = 0xFF;         
            SendBuff[1] = 0x00;         
            SendBuff[2] = 0x52;         
            SendBuff[3] = 0x00;         
            SendBuff[4] = 0x00;         

            SendLen = 0x05;
            RecvLen = 0x02;

            pioSendRequest.dwProtocol = Aprotocol;
            pioSendRequest.cbPciLength = 8;

            retCode = Card.SCardTransmit(hCard, ref pioSendRequest, ref SendBuff[0],
                                 SendLen, ref pioSendRequest, ref RecvBuff[0], ref RecvLen);

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                text = "Error";
                printMessage(2, text);
            }
            else
            {
                text = "Turn off success!";
                printMessage(1, text);
            }
            
        }

        private void turnOnBuzzerBtn_Click(object sender, EventArgs e)
        {
            string text = "";

            connectCard();

            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x52;
            SendBuff[3] = 0xFF;
            SendBuff[4] = 0x00;

            SendLen = 0x05;
            RecvLen = 0x02;

            pioSendRequest.dwProtocol = Aprotocol;
            pioSendRequest.cbPciLength = 8;

            retCode = Card.SCardTransmit(hCard, ref pioSendRequest, ref SendBuff[0],
                                 SendLen, ref pioSendRequest, ref RecvBuff[0], ref RecvLen);

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                text = "Error";
                printMessage(2, text);
            }
            else
            {
                text = "Turn on success!";
                printMessage(1, text);
            }
        }

        private void writeBtn_Click(object sender, EventArgs e)
        {
            if (connectCard())
            {
                string text = textBoxInput.Text;
                int block = (int)numericWriteSector.Value;
                WriteText(text, block);
            }
        }

        private void readBlockBtn_Click(object sender, EventArgs e)
        {
            int block = (int)numericReadSector.Value;
            string text = ReadBlock(block);
            string msg = "Read block " + (block).ToString() + ": ";
            printMessage(0, msg);
            printMessage(0, text);
        }

        private void readAllSectorBtn_Click(object sender, EventArgs e)
        {
            string text = ReadAllBlock();
            printMessage(0, text);
        }

        private void formatBtn_Click(object sender, EventArgs e)
        {
            if (connectCard())
            {
                string text = "";
                for (int block = 0; block < 64; block++)
                {
                    WriteText(text, block);
                }
            }
        }

        private async void CardState()
        {

            connectCard();

            while (true)
            {
                await Task.Run(() =>
                {
                    int readerLen = readername.Length;
                    int cardState = 0;
                    byte[] atr = new byte[256];
                    int atrLen = atr.Length;
                    int protocol = 0;

                    retCode = Card.SCardStatus(hContext, readername, ref readerLen, ref cardState, ref protocol, ref atr[0], ref atrLen);
                    Console.WriteLine(retCode);
                    Console.WriteLine(atrLen);
                    if (retCode == Card.SCARD_PRESENT)
                    {
                        Console.WriteLine("ADA");
                    }
                    else
                    {
                        Console.WriteLine("TIDAK!");
                    }
                });
            }

        }

        public bool connectCard()
        {
            connActive = true;

            retCode = Card.SCardConnect(hContext, readername, Card.SCARD_SHARE_SHARED,
                      Card.SCARD_PROTOCOL_T0 | Card.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                msg = "Card not connected";
                printMessage(2, msg);
                connActive = false;
                return false;
            }

            int readerLen = readername.Length;
            int cardState = 0;
            byte[] atr = new byte[256];
            int atrLen = atr.Length;
            int protocol = 0;

            retCode = Card.SCardStatus(hContext, readername, ref readerLen, ref cardState, ref protocol, ref atr[0], ref atrLen);

            return true;
        }

        private string ReadAllBlock()
        {
            string result = "";
            string value = "";
            if (connectCard())
            {
                for (int i=0; i<64; i++)
                {
                    value = ReadBlockValue(i);
                    value = value.Split(new char[] { '\0' }, 2, StringSplitOptions.None)[0].ToString();
                    value = i + " => " + value + "\n";
                    result += value;
                }
            }

            return result;


            //if (connectCard())
            //{
            //    string tmpStr = "";
            //    int indx;

            //    if (authenticateAllBlock())
            //    {
            //        ClearBuffers();
            //        SendBuff[0] = 0xFF;         // CLA 
            //        SendBuff[1] = 0xB0;         // INS
            //        SendBuff[2] = 0x00;         // P1
            //        SendBuff[3] = 0x00;  // P2 : Block No.
            //        SendBuff[4] = 0x10;         // Length

            //        SendLen = 5;
            //        RecvLen = SendBuff[4] + 2;

            //        for (int i = 0; i < 60; i++)
            //        {
            //            //Console.WriteLine("masuk " + i);
            //            retCode = SendAPDUandDisplay(2);

            //            if (retCode == -200)
            //            {
            //                return "outofrangeexception";
            //            }

            //            if (retCode == -202)
            //            {
            //                return "BytesNotAcceptable";
            //            }

            //            if (retCode != Card.SCARD_S_SUCCESS)
            //            {
            //                return "FailRead";
            //            }

            //            // Display data in text format
            //            for (indx = 0; indx <= RecvLen - 1; indx++)
            //            {
            //                tmpStr = tmpStr + Convert.ToChar(RecvBuff[indx]);
            //            }
            //            //Console.WriteLine(tmpStr);
            //            tmpStr += "\n";

            //            SendBuff[3] = (byte)i;
            //        }

            //        return (tmpStr);
            //    }
            //    else
            //    {
            //        return "FailAuthentication";
            //    }
            //}
            //return "Card not connected";
        }

        private string ReadBlock(int block)
        {
            string value = "";
            if (connectCard())
            {
                value = ReadBlockValue(block);
            }

            value = value.Split(new char[] { '\0' }, 2, StringSplitOptions.None)[0].ToString();

            return value;
        }

        private string ReadBlockValue(int block)
        {
            string tmpStr = "";
            int indx;

            if (authenticateBlock(block))
            {
                ClearBuffers();
                SendBuff[0] = 0xFF;         // CLA 
                SendBuff[1] = 0xB0;         // INS
                SendBuff[2] = 0x00;         // P1
                SendBuff[3] = (byte)block;  // P2 : Block No.
                SendBuff[4] = 0x10;         // Length

                SendLen = 5;
                RecvLen = SendBuff[4] + 2;

                retCode = SendAPDUandDisplay(2);

                if (retCode == -200)
                {
                    return "outofrangeexception";
                }

                if (retCode == -202)
                {
                    return "BytesNotAcceptable";
                }

                if (retCode != Card.SCARD_S_SUCCESS)
                {
                    return "FailRead";
                }

                // Display data in text format
                for (indx = 0; indx <= RecvLen - 1; indx++)
                {
                    tmpStr = tmpStr + Convert.ToChar(RecvBuff[indx]);
                }

                return (tmpStr);
            }
            else
            {
                return "FailAuthentication";
            }
        }

        private void WriteText (string text, int block)
        {
            string tmpStr = text;
            int indx;
            if (authenticateBlock(block))
            {
                ClearBuffers();
                SendBuff[0] = 0xFF;                             // CLA
                SendBuff[1] = 0xD6;                             // INS
                SendBuff[2] = 0x00;                             // P1
                SendBuff[3] = (byte)block;                     // P2 : Starting Block No.
                SendBuff[4] = 0x10;                             // P3 : Data length

                for (indx = 0; indx <= (tmpStr).Length - 1; indx++)
                {
                    SendBuff[indx + 5] = (byte)tmpStr[indx];
                }
                SendLen = SendBuff[4] + 5;
                RecvLen = 0x02;

                retCode = SendAPDUandDisplay(2);

                if (retCode != Card.SCARD_S_SUCCESS)
                {
                    msg = "write failed!";
                    printMessage(2, msg);
                }
                else
                {
                    msg = "write success!";
                    printMessage(1, msg);
                }
            }
            else
            {
                msg = "Authentication failed!";
                printMessage(2, msg);
            }
        }

        // block authentication
        private bool authenticateBlock(int block)
        {
            ClearBuffers();
            SendBuff[0] = 0xFF;                         // CLA
            SendBuff[1] = 0x86;                         // INS: for stored key input
            SendBuff[2] = 0x00;                         // P1: same for all source types 
            SendBuff[3] = 0x00;                         // P2 : Memory location;  P2: for stored key input
            SendBuff[4] = 0x05;                         // P3: for stored key input
            SendBuff[5] = 0x01;                         // Byte 1: version number
            SendBuff[6] = 0x00;                         // Byte 2
            SendBuff[7] = (byte)block;       // Byte 3: blocke no. for stored key input
            SendBuff[8] = 0x60;                         // Byte 4 : Key A for stored key input
            SendBuff[9] = 0x01;         // Byte 5 : Session key for non-volatile memory

            SendLen = 0x0A;
            RecvLen = 0x02;

            retCode = SendAPDUandDisplay(0);

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                //MessageBox.Show("FAIL Authentication!");
                return false;
            }

            return true;
        }

        private bool authenticateAllBlock()
        {
            ClearBuffers();
            SendBuff[0] = 0xFF;                         // CLA
            SendBuff[1] = 0x86;                         // INS: for stored key input
            SendBuff[2] = 0x00;                         // P1: same for all source types 
            SendBuff[3] = 0x00;                         // P2 : Memory location;  P2: for stored key input
            SendBuff[4] = 0x05;                         // P3: for stored key input
            SendBuff[5] = 0x01;                         // Byte 1: version number
            SendBuff[6] = 0x00;                         // Byte 2
            SendBuff[7] = 0x00;       // Byte 3: blocke no. for stored key input
            SendBuff[8] = 0x60;                         // Byte 4 : Key A for stored key input
            SendBuff[9] = 0x01;         // Byte 5 : Session key for non-volatile memory

            SendLen = 0x0A;
            RecvLen = 0x02;

            for (int i = 0; i < 64; i++)
            {
                retCode = SendAPDUandDisplay(0);

                if (retCode != Card.SCARD_S_SUCCESS)
                {
                    Console.WriteLine("GAGAL AUTH");
                    return false;
                }

                Console.WriteLine("AUTH " + i);

                SendBuff[7] = (byte)i;
            }

            return true;
        }

        // clear memory buffers
        private void ClearBuffers()
        {
            long indx;

            for (indx = 0; indx <= 262; indx++)
            {
                RecvBuff[indx] = 0;
                SendBuff[indx] = 0;
            }
        }

        // send application protocol data unit : communication unit between a smart card reader and a smart card
        private int SendAPDUandDisplay(int reqType)
        {
            int indx;
            string tmpStr = "";

            pioSendRequest.dwProtocol = Aprotocol;
            pioSendRequest.cbPciLength = 8;

            retCode = Card.SCardTransmit(hCard, ref pioSendRequest, ref SendBuff[0],
                                 SendLen, ref pioSendRequest, ref RecvBuff[0], ref RecvLen);

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                return retCode;
            }

            else
            {
                try
                {
                    tmpStr = "";
                    switch (reqType)
                    {
                        case 0:
                            for (indx = (RecvLen - 2); indx <= (RecvLen - 1); indx++)
                            {
                                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                            }

                            if ((tmpStr).Trim() != "90 00")
                            {
                                //MessageBox.Show("Return bytes are not acceptable.");
                                return -202;
                            }

                            break;

                        case 1:

                            for (indx = (RecvLen - 2); indx <= (RecvLen - 1); indx++)
                            {
                                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);
                            }

                            if (tmpStr.Trim() != "90 00")
                            {
                                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                            }

                            else
                            {
                                tmpStr = "ATR : ";
                                for (indx = 0; indx <= (RecvLen - 3); indx++)
                                {
                                    tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                                }
                            }

                            break;

                        case 2:

                            for (indx = 0; indx <= (RecvLen - 1); indx++)
                            {
                                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                            }

                            break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    return -200;
                }
            }
            return retCode;
        }

        //disconnect card reader connection
        public void Close()
        {
            if (connActive)
            {
                retCode = Card.SCardDisconnect(hCard, Card.SCARD_UNPOWER_CARD);
            }
            //retCode = Card.SCardReleaseContext(hCard);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string nrp = textBoxNrp.Text;
            string name = textBoxName.Text;

            connectCard();

            if (name.Length > 16 )
            {
                // simpan dipisah
                string name1 = name.Substring(0, 16);
                string name2 = name.Substring(16, (name.Length - 16));
                WriteText(nrp, 4);
                WriteText(name1, 8);
                WriteText(name2, 9);
            }
            else
            {
                WriteText(nrp, 4);
                WriteText(name, 8);
            }
            msg = "NRP: " + nrp + " Name: " + name;
            printMessage(0, msg);
            db.Insert(nrp, name);
        }

        private void readBtn_Click(object sender, EventArgs e)
        {
            List<string> mhs;
            string nrp;

            connectCard();

            nrp = ReadBlock(4);
            mhs = db.SelectNrp(nrp);
            if (mhs.Any())
            {
                msg = "Data yang tersimpan adalah: \n"
                + "NRP: " + mhs[1] + "\n"
                + "Name: " + mhs[2];
                printMessage(0, msg);
            }
            else
            {
                msg = "Data tidak ada di database";
                printMessage(0, msg);
            }

        }

        private string getcardUID() //only for mifare 1k cards
        {
            string cardUID = "";
            byte[] receivedUID = new byte[256];
            Card.SCARD_IO_REQUEST request = new Card.SCARD_IO_REQUEST();
            request.dwProtocol = Card.SCARD_PROTOCOL_T1;
            request.cbPciLength = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Card.SCARD_IO_REQUEST));
            byte[] sendBytes = new byte[] { 0xFF, 0xCA, 0x00, 0x00, 0x00 }; //get UID command for Mifare cards
            int outBytes = receivedUID.Length;
            int status = Card.SCardTransmit(hCard, ref request, ref sendBytes[0], sendBytes.Length, ref request, ref receivedUID[0], ref outBytes);

            if (status != Card.SCARD_S_SUCCESS)
            {
                cardUID = "Error";
                string msg = "Error get UID";
                printMessage(2, msg);
            }
            else
            {
                cardUID = BitConverter.ToString(receivedUID.Take(4).ToArray()).Replace("-", string.Empty).ToLower();
            }

            return cardUID;
        }

        public void SelectDevice()
        {
            List<string> availableReaders = this.ListReaders();
            this.RdrState = new Card.SCARD_READERSTATE();
            this.readername = availableReaders[0].ToString(); //selecting first device
            this.RdrState.RdrName = this.readername;
        }

        public List<string> ListReaders()
        {
            int ReaderCount = 0;
            List<string> AvailableReaderList = new List<string>();

            //Make sure a context has been established before
            //retrieving the list of smartcard readers.
            retCode = Card.SCardListReaders(hContext, null, null, ref ReaderCount);
            if (retCode != Card.SCARD_S_SUCCESS)
            {
                MessageBox.Show(Card.GetScardErrMsg(retCode));
                connActive = false;
            }

            byte[] ReadersList = new byte[ReaderCount];

            //Get the list of reader present again but this time add sReaderGroup, retData as 2rd & 3rd parameter respectively.
            retCode = Card.SCardListReaders(hContext, null, ReadersList, ref ReaderCount);
            if (retCode != Card.SCARD_S_SUCCESS)
            {
                MessageBox.Show(Card.GetScardErrMsg(retCode));
            }

            string rName = "";
            int indx = 0;
            if (ReaderCount > 0)
            {
                //Convert reader buffer to string
                while (ReadersList[indx] != 0)
                {

                    while (ReadersList[indx] != 0)
                    {
                        rName = rName + (char)ReadersList[indx];
                        indx = indx + 1;
                    }

                    //Add reader name to list
                    AvailableReaderList.Add(rName);
                    rName = "";
                    indx = indx + 1;

                }
            }

            Console.WriteLine("Reader in byte: ");
            foreach (byte r in ReadersList)
            {
                Console.Write(r + " ");
            }

            Console.WriteLine("\nReader list:");
            foreach (string reader in AvailableReaderList)
            {
                Console.WriteLine(reader);
            }

            return AvailableReaderList;

        }

        internal void establishContext()
        {
            retCode = Card.SCardEstablishContext(Card.SCARD_SCOPE_SYSTEM, 0, 0, ref hContext);
            if (retCode != Card.SCARD_S_SUCCESS)
            {
                MessageBox.Show("Check your device and please restart again", "Reader not connected", MessageBoxButtons.OK);
                connActive = false;
                return;
            }
            else
            {
                Console.WriteLine("MASUK");
            }
        }

        private void printMessage(int errType, string message)
        {
            switch (errType)
            {
                case 0:
                    messageBox.SelectionColor = Color.Black;
                    break;
                case 1:
                    messageBox.SelectionColor = Color.Green;
                    break;
                case 2:
                    messageBox.SelectionColor = Color.Red;
                    break;
                default:
                    messageBox.SelectionColor = Color.Black;
                    break;
            }

            messageBox.AppendText(message);
            messageBox.AppendText("\n");
            messageBox.SelectionColor = Color.Black;
            messageBox.Focus();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            messageBox.Clear();
        }
    }
}
