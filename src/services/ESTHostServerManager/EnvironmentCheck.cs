/**********************************************************************
*******命名空间： ESTHost.ServerManager
*******类 名 称： EnvironmentCheck
*******类 说 明： 环境检测
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/13/2021 4:58:26 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.ServerManager
{
    /// <summary>
    ///  
    /// </summary>
    public class EnvironmentCheck
    {
        /// <summary>
        /// 数据库检查
        /// </summary>
        /// <returns></returns>
        public static Task<CheckItem> DatabaseCheck()
        {
           return Task.Run(() =>
            {
                var item = new CheckItem();
                if(TestConnection("172.16.1.22", 5432, 2000))
                {
                    item.Status = StatusType.Success;
                    item.Info = "数据库连接成功！";
                }
                else
                {
                    item.Status = StatusType.Error;
                    item.Info = "数据库连接失败！";
                }
                return Task.FromResult(item);
            });
        }

        public static bool TestConnection(string host, int port, int millisecondsTimeout)
        {
            TcpClient client = new TcpClient();
            try
            {
                var ar = client.BeginConnect(host, port, null, null);
                ar.AsyncWaitHandle.WaitOne(millisecondsTimeout);
                return client.Connected;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                client.Close();
            }
        }
    }
}
