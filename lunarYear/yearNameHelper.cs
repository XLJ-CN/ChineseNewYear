using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lunarYear
{
    public static class yearNameHelper
    {
        static string[] tianGan = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
        static string[] diZhi = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };

        static string[] month = { "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "冬月", "腊月" };
        static string[] day = { "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十", "十一", "十二", "十三", "十四", "十五"
        , "十六", "十七", "十八", "十九", "二十", "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十"
        };


        public static string getLunarYearName(int year)
        {
            //1984年是庚辰年 也就是tianGan[0] diZhi[0]
            if (year < 1984)
            {
                return "";
            }
            else
            {
                int span = year - 1984; //假设传入的是1985年
                int tianganOffset = span % 10;            // 1
                int dizhiOffset = span % 12;// 1
                int tianganStart = 0;
                int dizhiStart = 0;
                tianganStart += tianganOffset;
                dizhiStart += dizhiOffset;
                return tianGan[tianganStart] + diZhi[dizhiStart];
                //return "甲子";
            }

        }

        public static string getMonthName(int m)
        {
            if (m <= 0)
            {
                return month[m];
            }
            return month[m - 1];
        }
        public static string getDayName(int m)
        {
            return day[m - 1];
        }
    }
}
