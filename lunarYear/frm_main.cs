
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.CompilerServices;
namespace lunarYear
{
    public partial class frm_main : Form
    {
        string lunarDayFull = "";
        TimeSpan yearTimeSpan;
        string animal = "兔";
        public frm_main()
        {
            InitializeComponent();
        }

        private void frm_main_Load(object sender, EventArgs e)
        {
            int year = 2004;
            var lunarName = yearNameHelper.getLunarYearName(year);

            //获取今天所处的农历是多少
            lunarDayFull = getThisLunar();
            label2.Text = lunarDayFull;
            caculateTimeSpan();
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            //刷新农历
            //获取今天所处的农历是多少
            lunarDayFull = getThisLunar();
            label2.Text = lunarDayFull;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // yearTimeSpan需要减去1秒
            var new_yearTimeSpan = yearTimeSpan.TotalSeconds - 1;
            //刷新倒计时
            // caculate();
            var remains = yearTimeSpan;
            var remainDays = remains.Days;
            var remainHours = remains.Hours;
            var remainMin = remains.Minutes;
            var remainSec = remains.Seconds;
            label3.Text = $"距离农历新年 {animal}年还有 {remainDays}天{remainHours}小时{remainMin}分{remainSec}秒";
            TimeSpan theNew = TimeSpan.FromSeconds(new_yearTimeSpan);
            yearTimeSpan = theNew;
        }

        public void caculateTimeSpan()
        {

            //农历年月日
            var lunarDay = lunarDayFull;//.Substring(0, lunarDayFull.IndexOf("日"));//2023年12月10日 农历
            //正则提取出数字 按照顺序关系就是年月日了
            var resgex = new Regex(@"\d+");
            var allMatch = resgex.Matches(lunarDay);
            var thisLunarYear = allMatch[0].Value;
            var nextLunarYear = int.Parse(thisLunarYear) + 1;
            //新的一年一月一日的公元纪年是多少         
            var newYearSolarDate = getSolar(nextLunarYear);//2024-02-10日
            //过年的时间减去当前时间
            var remains = newYearSolarDate - DateTime.Now;
            yearTimeSpan = remains;

        }

        /// <summary>
        ///  获取指定年份农历大年初一的阳历时间
        /// </summary>
        /// <returns></returns>
        private DateTime getSolar(int year)
        {
            try
            {
                animal = getAnimal(year);
                ChineseLunisolarCalendar cnsc = new ChineseLunisolarCalendar();
                DateTime ylDate = cnsc.ToDateTime(year, 1, 1, 0, 0, 0, 0);
                return ylDate;//.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                //   return "";
            }
            return DateTime.Now;
        }
        /// <summary>
        /// 返回当前时间的农历年月日
        /// </summary>
        /// <returns>返回形如 2023-12-13</returns>
        public string getThisLunar()
        {
            ChineseLunisolarCalendar cnsc = new ChineseLunisolarCalendar();
            var now = DateTime.Now;
            DateTime jrLDate = new DateTime(now.Year, now.Month, now.Day);//阳历时间
            int year = cnsc.GetYear(jrLDate);
            // 是否有闰月,返回正整数（比如2023年闰2月，返回值为3）
            int flag = cnsc.GetLeapMonth(year);
            //有闰月则实际月份减1
            //TODO 2025年闰6月 不好处理
            int month = cnsc.GetMonth(jrLDate);
            if (flag > 0)
            {
                if (flag - 1 == month)
                {
                    //闰月时间等于当前农历月份
                    month = cnsc.GetMonth(jrLDate) - 1;
                }
            }
            int day = cnsc.GetDayOfMonth(jrLDate);
            //计算下农历纪年法的叫法
            var year_lanar = yearNameHelper.getLunarYearName(year);
            var month_lanar = yearNameHelper.getMonthName(month);
            var day_lanar = yearNameHelper.getDayName(day);


            label5.Text = $"{year_lanar}年{month_lanar}{day_lanar}";
            return $"{year}-{month}-{day}";
        }



        public string getAnimal(int year)
        {
            string[] animals = { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };

            if (year > 1900)
            {
                year = year - 1900;
                int i = year % 12;
                return animals[i];
            }
            return "兔";

        }

    }


}
