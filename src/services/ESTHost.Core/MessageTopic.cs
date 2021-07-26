﻿using System;

namespace ESTHost.Core
{
    public class MessageTopic
    {
        public static readonly string Alert = "ALERT";
        public static readonly string Iot = "IOT";
        public static readonly string Realtime = "RELARTIME";
        public static readonly string Command = "COMMAND";
        public static readonly string AlertCommand = "ALERTCOMMAND";
        public static readonly string Sms = "SMS";
        public static readonly string SmsCommand = "SMSCOMMAND";
        public static readonly string Storage = "STORAGE";
        public static readonly string StorageCommand = "STORAGECOMMAND";
        public static readonly string Notice = "NOTICE";
    }
}
