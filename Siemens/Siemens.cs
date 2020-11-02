using S7TCPDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens
{
    /// <summary>
    /// 西门子
    /// </summary>
    public class Siemens
    {
        private S7Client plc = null;
        #region 连接PLC IP
        /// <summary>
        /// 连接PLC IP
        /// </summary>
        /// <param name="s7">S7Client 实例的对象</param>
        /// <param name="ip">IP</param>
        /// <param name="Rack">机架 默认：0</param>
        /// <param name="Slot">插槽 默认：1</param>
        /// <returns></returns>
        public int Open(ref S7Client s7, string ip, int Rack = 0, int Slot = 1)
        {
            plc = s7;
            int flag = plc.ConnectTo(ip, Rack, Slot);
            if (flag == 0)
            {
                return 0;
            }
            return 1;
        }
        #endregion
        #region DB读取
        /// <summary>
        /// DB读取byte
        /// </summary>
        /// <param name="db">地区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">数据长度</param>
        /// <returns>string</returns>
        public string DBRead_Byte(int db,int offset,int size = 64) {
            return Convert.ToString(plc.DBRead_BYTE(db, size, offset),16);
        }
        /// <summary>
        /// DB读取byte
        /// </summary>
        /// <param name="db">地区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">数据长度</param>
        /// <returns>string</returns>
        public string DBRead_Sint(int db, int offset, int size = 64)
        {
            return Convert.ToString(plc.DBRead_SINT(db, size, offset), 16);
        }
        #endregion
        #region DB读取
        public void DBWrite_() {
           
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

    }
}
