using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraceTalker.Core.Helpers
{
    public static class TelnetHelper
    {
        //Telnet Commands
        public static byte cmdSE = 0xF0;
        public static byte cmdNOP = 0xF1;
        public static byte cmdDM = 0xF2;
        public static byte cmdBRK = 0xF3;
        public static byte cmdIP = 0xF4;
        public static byte cmdAO = 0xF5;
        public static byte cmdAYT = 0xF6;
        public static byte cmdEC = 0xF7;
        public static byte cmdEL = 0xF8;
        public static byte cmdGA = 0xF9;
        public static byte cmdSB = 0xFA;

        public static byte cmdWILL = 0xFB;
        public static byte cmdWONT = 0xFC;
        public static byte cmdDO = 0xFD;
        public static byte cmdDONT = 0xFE;
        public static byte cmdIAC = 0xFF;

        //Telnet Options
        public static byte op_suppress_go_ahead = 0x03;
        public static byte op_status = 0x05;
        public static byte op_echo = 0x01;
        public static byte op_timing_mark = 0x06;
        public static byte op_terminal_type = 0x18;
        public static byte op_window_size = 0x1F;
        public static byte op_terminal_speed = 0x20;
        public static byte op_remote_flow_control = 0x21;
        public static byte op_linemode = 0x22;
        public static byte op_environment_variables = 0x24;
    }
}
