using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Class_IPv4
{
    
    public class IPv4
    {
        #region Attributes
        private int[] _numbs = new int[4];
        private int _netMask;
        #endregion

        #region Constructors
        public IPv4 ()
        {
            Random rnd = new Random();
            _numbs[0] = rnd.Next(0, 256);
            _numbs[1] = rnd.Next(0, 256);
            _numbs[2] = rnd.Next(0, 256);
            _numbs[3] = rnd.Next(0, 256);
            _netMask = rnd.Next(0, 32);
        }
        public IPv4(int a, int b, int c, int d, int e)                                                                    
        {
            Number1 = a;
            Number2 = b;
            Number3 = c;
            Number4 = d;
            NetMask = e;
        }
        public IPv4(IPv4 i)
        {
            Number1 = i._numbs[0]; Number2 = i._numbs[1]; Number3 = i._numbs[2]; Number4 = i._numbs[3];
            NetMask = i._netMask;
        }
        #endregion

        #region Properties
        public int Number1
        {
            get { return _numbs[0]; }
            set
            {
                if (value >= 0 && value < 256)
                {
                    _numbs[0] = value;
                }
                else { _numbs[0]= -1; }
            }
        }

        public int Number2
        {
            get { return _numbs[1]; }
            set
            {
                if (value >= 0 && value < 256)
                {
                    _numbs[1] = value;
                }
                else { _numbs[1] = -1; }
            }
        }

        public int Number3
        {
            get { return _numbs[2]; }
            set
            {
                if (value >= 0 && value < 256)
                {
                    _numbs[2] = value;
                }
                else { _numbs[2] = -1; }
            }
        }

        public int Number4
        {
            get { return _numbs[3]; }
            set
            {
                if (value >= 0 && value < 256)
                {
                    _numbs[3] = value;
                }
                else { _numbs[3] = -1; }
            }
        }

        public int NetMask
        {
            get { return _netMask; }
            set
            {
                if (value >= 0 && value < 33)
                {
                    _netMask = value;
                }
                else { _netMask = -1; }
            }
        }
        #endregion

        #region Methods

        public bool validateIPv4()
        {
            if (Number1 >= 0 && Number1 < 256 && Number2 >= 0 && Number2 < 256 && Number3 >= 0 && Number3 < 256 && Number4 >= 0 && Number4 < 256 && NetMask >= 0 && NetMask < 33)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string checkClassofIPv4()
        {
            if(Number1 >= 0 && Number1 < 128) 
            {
                return "A";
            }
            else if (Number1 >= 128 && Number1 < 192)
            {
                return "B";
            }
            else if (Number1 >= 192 && Number1 < 224)
            {
                return "C";
            }
            else
            {
                return ("Another class.");
            }
        }

        public string checkPrivateOrPublic()
        {
            if(Number1 == 10 || Number1 == 172 && Number2 >= 16 && Number2 < 32 || Number1 == 192 && Number2 == 168)
            {
                return "Private";
            }
            else
            {
                return "Public";
            }
        }
        public string checkConnectivity(IPv4 newIP)
        {
            if (Number1 == newIP.Number1 && Number2 == newIP.Number2 && Number3 == newIP.Number3 && NetMask == newIP.NetMask)
            {
                return "The IPs are in the same Network.";
            }
            else
            {
                return "The IPs are not in the same Network.";
            }
        }
            

        public override string ToString()
        {
            return $"{Number1}.{Number2}.{Number3}.{Number4}.{NetMask}";
        }

        public string printToConsole()
        {
            return $"{Number1}.{Number2}.{Number3}.{Number4}/{NetMask}";
        }
        #endregion
    }
}
