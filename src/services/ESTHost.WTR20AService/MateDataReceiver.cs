/**********************************************************************
*******命名空间： ESTHost.WTR20AService
*******类 名 称： MateDataReceiver
*******类 说 明： 元数据接收机，通过协议进行解析
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 1:46:24 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTHost.Core.Colleaction;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESTHost.WTR20AService
{
    /// <summary>
    ///  原始数据接收类
    /// </summary>
    public class MateDataReceiver : AbstractEventBus
    {
        /**
         * 主要业务：
         * 1、通过请求的结果进行判断，再根据功能码判断如何将消息组装并发送
         * 2、解析完数据，第一次存储温度值，结合容错温度进行处理温度是否发送
         * 3、将最终完整的数据以消息的方式发送到数据中心，再由数据中心进行精加工
         */
        public MateDataReceiver()
        {

        }
        /// <summary>
        /// 接收到数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Task<bool> ReceiverMateData(OperateResult result)
        {

            return Task.FromResult(true);
        }
    }
}
