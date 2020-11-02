using AxActUtlTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MitsubishiHelper
{
    /// <summary>
    /// 三菱PLC 网口
    /// 使用的逻辑站号，
    /// </summary>
    public class Ethernet
    {
        #region 公共变量
        public static AxActUtlType _axActUtlType = null;
        private static Ethernet _Helper;
        #endregion
        #region 单例
        /// <summary>
        /// 单例
        /// </summary>
        public static Ethernet Helper
        {
            get
            {
                if (_Helper != null)
                {

                    _Helper = new Ethernet();
                }
                return _Helper;
            }
        }
        #endregion
        #region 开启PLC
        /// <summary>
        /// 开启PLC
        /// </summary>
        /// <param name="axActUtlType"> AxActUtlType</param>
        /// <param name="_StationNo"></param>
        /// <param name="_Password">密码</param>
        /// <returns></returns>
        public  string Open(Object form, ref AxActUtlType axActUtlType, int _StationNo = 0, string _Password = "")
        {
            try
            {
                ((Form)form).Controls.Add(axActUtlType);                
                _axActUtlType = axActUtlType;               
                axActUtlType.ActLogicalStationNumber = _StationNo;
                _axActUtlType.ActPassword = _Password;
                int rtn = _axActUtlType.Open();
                if (rtn == 0)
                {
                    return "PLC连接成功";
                }
                else
                {
                    return "PLC连接失败";
                }
            }
            catch (Exception)
            {
                
                return "PLC连接失败";
            }
        }
        #endregion
        #region 读取数据ReadDeviceBlock
        /// <summary>
        /// 读取数据 ReadDeviceBlock2
        /// </summary>
        /// <param name="szDevice"></param>
        /// <param name="lSize"></param>
        /// <returns></returns>
        public string ReadBlock2(string szDevice, string lSize = "2")
        {
            try
            {
                int num = Convert.ToInt32(lSize);
                short[] arr = new short[num];
                int rtn = _axActUtlType.ReadDeviceBlock2(szDevice, num, out arr[0]);
                string s = "";
                if (rtn == 0)
                {
                    for (int i = arr.Length - 1; i >= 0; i--)
                    {
                        s += string.Format("{0:X4}", arr[i]);
                    }
                    return (Convert.ToInt32(s, 16).ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 读取数据  ReadDeviceBlock
        /// </summary>
        /// <param name="szDevice"></param>
        /// <param name="lSize"></param>
        /// <returns></returns>
        public string ReadBlock(string szDevice, string lSize = "2")
        {
            try
            {
                int num = Convert.ToInt32(lSize);
                int[] arr = new int[num];
                int rtn = _axActUtlType.ReadDeviceBlock(szDevice, num, out arr[0]);
                string s = "";
                if (rtn == 0)
                {
                    for (int i = arr.Length - 1; i >= 0; i--)
                    {
                        s += string.Format("{0:X4}", arr[i]);
                    }
                    return (Convert.ToInt32(s, 16).ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region 写入WriteDeviceBlock
        /// <summary>
        /// 将数据写入PLC
        /// </summary>
        /// <param name="szDevice">地区</param>
        /// <param name="strData">数据</param>
        /// <returns></returns>
        public string WriteDeviceBlock2(string szDevice, string[] strData)
        {
            try
            {
                int num = strData.Length;
                short[] arr = new short[num];
                for (int i = 0; i < num; i++)
                {
                    arr[i] = Convert.ToInt16(strData[i]);
                }
                int rtn = _axActUtlType.WriteDeviceBlock2(szDevice, num, ref arr[0]);
                if (rtn == 0)
                {
                    return (num.ToString()) + "成功写入";
                }
                else
                {
                    return "写入数据失败";
                }

            }
            catch (Exception)
            {
                return "写入数据失败";
            }
        }
        /// <summary>
        /// 将数据写入PLC
        /// </summary>
        /// <param name="szDevice">地区</param>
        /// <param name="strData">数据</param>
        /// <returns></returns>
        public string WriteDeviceBlock(string szDevice, string[] strData)
        {
            try
            {
                int num = strData.Length;
                int[] arr = new int[num];
                for (int i = 0; i < num; i++)
                {
                    arr[i] = Convert.ToInt32(strData[i]);
                }
                int rtn = _axActUtlType.WriteDeviceBlock(szDevice, num, ref arr[0]);
                if (rtn == 0)
                {
                    return (num.ToString()) + "成功写入";
                }
                else
                {
                    return "写入数据失败";
                }

            }
            catch (Exception)
            {
                return "写入数据失败";
            }
        }
        #endregion
        #region  读取ReadRandom
        /// <summary>
        /// 读取ReadRandom
        /// </summary>
        /// <param name="szDevice"></param>
        /// <param name="lSize"></param>
        /// <returns></returns>
        public string  ReadRandom(string szDevice, string lSize = "2") {
            try
            {
                
                int num = Convert.ToInt32(lSize);
                int[] arr = new int[num];               
                int rtn = _axActUtlType.ReadDeviceRandom(szDevice, num, out arr[0]); ;
                string s = "";
                if (rtn == 0)
                {
                    for (int i = arr.Length - 1; i >= 0; i--)
                    {
                        s += string.Format("{0:X4}", arr[i]);
                    }
                    return (Convert.ToInt32(s, 16).ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 读取ReadRandom2
        /// </summary>
        /// <param name="szDevice"></param>
        /// <param name="lSize"></param>
        /// <returns></returns>
        public string ReadRandom2(string szDevice, string lSize = "2")
        {
            try
            {

                int num = Convert.ToInt32(lSize);
                short[] arr = new short[num];
                int rtn = _axActUtlType.ReadDeviceRandom2(szDevice, num, out arr[0]);
                string s = "";
                if (rtn == 0)
                {
                    for (int i = arr.Length - 1; i >= 0; i--)
                    {
                        s += string.Format("{0:X4}", arr[i]);
                    }
                    return (Convert.ToInt32(s, 16).ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region
        #endregion
        #region PLC高低位
        /// <summary>
        /// PLC高低位
        /// </summary>
        /// <param name="twoWord"></param>
        /// <param name="highWord"></param>
        /// <param name="lowWord"></param>
        public void Hi_lo(string twoWord, out string highWord, out string lowWord)
        {
            if (twoWord.Contains("-"))
            {

                int low = int.Parse(twoWord) % 65536;

                int high = -1;
                lowWord = low.ToString();
                highWord = high.ToString();
            }
            else
            {
                int low = int.Parse(twoWord) % 65536;

                int high = int.Parse(twoWord) / 65536;
                lowWord = low.ToString();
                highWord = high.ToString();
            }
        }
        #endregion
        
        #region 关闭
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public static bool Close()
        {
            return _axActUtlType.Close() == 0;
        }
        #endregion 
        #region 读GetDevice
        /// <summary>
        /// 读bit GetDevice2
        /// </summary>
        /// <param name="szDevice">地区</param>
        /// <returns>string</returns>
        public string GetDevice2(string szDevice)
        {
            short lpsData;
            try
            {
                 
                _axActUtlType.GetDevice2(szDevice, out lpsData);
            }
            catch (Exception)
            {

                return null;
            }
            return lpsData.ToString();
        }
        /// <summary>
        /// 读bit GetDevice
        /// </summary>
        /// <param name="szDevice"></param>        
        /// <returns></returns>
        public string GetDevice(string szDevice) {
            int lpsData;
            try
            {

                _axActUtlType.GetDevice(szDevice, out lpsData);
            }
            catch (Exception)
            {

                return null;
            }
            return lpsData.ToString();
        }
        #endregion 
        #region 写GetDevice2
        /// <summary>
        /// 写bit SetDevice2
        /// </summary>
        /// <param name="szDevice">地区</param>
        /// <param name="sData">数据</param>
        /// <returns></returns>
        public bool SetDevice2(string szDevice, short sData)
        {
            try
            {
                _axActUtlType.SetDevice2(szDevice, sData);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        /// <summary>
        /// 写bit SetDevice
        /// </summary>
        /// <param name="szDevice">地区</param>
        /// <param name="sData">数据</param>
        /// <returns></returns>
        public bool SetDevice(string szDevice, int sData)
        {
            try
            {
                _axActUtlType.SetDevice(szDevice, sData);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        #endregion
        #region

        #endregion
        #region
        #endregion
        #region
        #endregion
        #region
        #endregion
        #region
        #endregion
        #region
        #endregion
        #region
        #endregion
    }
}
