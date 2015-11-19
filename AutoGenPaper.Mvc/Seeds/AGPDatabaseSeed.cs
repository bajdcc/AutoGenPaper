using AutoGenPaper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using WebMatrix.WebData;

namespace AutoGenPaper.Mvc
{
	public class AGPDatabaseSeed
	{
		private static Question GenQuestion(Random rand, Question question)
		{
			var q = new Question();
			q.TypeId = question.TypeId;
			q.Info = new ModelDescription() { Name = question.Info.Name, Description = question.Info.Description };
			q.Time = new ModelTime();
			q.Option = question.Option;
			q.Answer = question.Answer;
			q.Owner = question.Owner;
			q.Catalog = question.Catalog;
			q.Points = question.Points;
			q.Difficulty = rand.Next(10) + 1;
			q.Label = question.Label;
			return q;
		}

		public static void Seed(AGPDataContext context)
		{
			EnumHelper.GetFields(typeof(AGPDefine.LogLevelType)).ToList().ForEach(
				a => context.LogLevels.Add(new SystemLogLevel() { LevelName = EnumHelper.GetEnumDescription(a) }));
			EnumHelper.GetFields(typeof(AGPDefine.LogEventType)).ToList().ForEach(
				a => context.LogEvents.Add(new SystemLogEvent() { EventName = EnumHelper.GetEnumDescription(a) }));
			EnumHelper.GetFields(typeof(AGPDefine.LogObjectType)).ToList().ForEach(
				a => context.LogObjects.Add(new SystemLogObject() { ObjectName = EnumHelper.GetEnumDescription(a) }));

			context.SaveChanges();

			EnumHelper.GetFields(typeof(AGPDefine.RoleType)).ToList().ForEach(
				a => Roles.CreateRole(EnumHelper.GetEnumDescription(a)));

			WebSecurity.CreateUserAndAccount("admin", "admin", new { RealName = "管理员", State = (int)AGPDefine.CommitType.Normal });
			WebSecurity.CreateUserAndAccount("teacher", "teacher", new { RealName = "教师", State = (int)AGPDefine.CommitType.Normal });
			WebSecurity.CreateUserAndAccount("verifier", "verifier", new { RealName = "审核员", State = (int)AGPDefine.CommitType.Normal });
			WebSecurity.CreateUserAndAccount("researcher", "researcher", new { RealName = "教研员", State = (int)AGPDefine.CommitType.Normal });

			Roles.AddUsersToRole(new string[] { "admin" }, "管理员");
			Roles.AddUsersToRole(new string[] { "teacher" }, "教师");
			Roles.AddUsersToRole(new string[] { "verifier" }, "审核员");
			Roles.AddUsersToRole(new string[] { "researcher" }, "教研员");

			User user = context.Users.First(a => a.UserName == "teacher");
			User verifier = context.Users.First(a => a.UserName == "verifier");
			Course course = new Course()
			{
				Info = new ModelDescription() { Name = "信息技术" }
			};
			List<Catalog> catalog = new List<Catalog>()
			{
				new Catalog()
				{
					Course = course, Info = new ModelDescription() { Name = "基础知识" }
				},
				new Catalog()
				{
					Course = course, Info = new ModelDescription() { Name = "Windows基础" }
				},
				new Catalog()
				{
					Course = course, Info = new ModelDescription() { Name = "Word基础" }
				},
				new Catalog()
				{
					Course = course, Info = new ModelDescription() { Name = "Excel基础" }
				},
				new Catalog()
				{
					Course = course, Info = new ModelDescription() { Name = "PowerPoint基础" }
				},
				new Catalog()
				{
					Course = course, Info = new ModelDescription() { Name = "FrontPage基础" }
				},
				new Catalog()
				{
					Course = course, Info = new ModelDescription() { Name = "网络基础" }
				},
				new Catalog()
				{
					Course = course, Info = new ModelDescription() { Name = "程序设计" }
				},
			};
			context.Courses.Add(course);
			catalog.ForEach(a => context.Catalogs.Add(a));
			context.SaveChanges();

			List<IAGPQuestionModel> questions = new List<IAGPQuestionModel>()
			{
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列说法中错误的是（    ）。", Option = new List<string>()
					{
						"每一个子文件夹都有一个父文件夹", "每个文件夹都可以包含若干个子文件夹和文件", "根文件夹是自动存在的", "文件夹不能重名"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列设备中，（    ）既是输入设备，又是输出设备。", Option = new List<string>()
					{
						"鼠标", "键盘", "显示器", "触摸屏"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "自然码输入法输入（       ）。", Option = new List<string>()
					{
						"数字类", "字音类", "字形类", "混合类"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "我们通常所说的主机，其组成部分是指（     ）。", Option = new List<string>()
					{
						"硬件和软件", "控制器和存储器", "运算器和外设", "中央处理器和存储器"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列因素中，对微型计算机工作影响最小的是（     ）。", Option = new List<string>()
					{
						"磁场", "温度", "湿度", "噪声"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "计算机病毒是可以造成计算机故障的一种（     ）。", Option = new List<string>()
					{
						"计算机设备", "计算机芯片", "计算机部件", "计算机程序"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "计算机中CPU的任务是什么？", Option = new List<string>()
					{
						"执行存放在CPU中的指令序列", "执行存放在存储器中的语句", "执行存放在CPU中的语句", "执行存放在存储器中的指令序列"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中，将当前文档换名保存时，正确的操作是(        )。", Option = new List<string>()
					{
						"单击常用工具栏上的“保存”按钮", "选择“编辑”菜单中的“复制”命令", "选择“文件”菜单中的“保存”命令", "选择“文件”菜单中的“另存为…”命令"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Internet所采用的通信协议是(        )。", Option = new List<string>()
					{
						"CSMA/CD", "X.25", "令牌环", "TCP/IP"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "计算机图片的颜色中，(        )具有像黑白照片一样的效果。", Option = new List<string>()
					{
						"彩色", "灰度", "黑白", "单色"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "目前家用拨号上网的调制解调器的速率没有(        )。", Option = new List<string>()
					{
						"28.8k", "33.6k", "56k", "128K"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Internet最初创建的目的是用于(        )。", Option = new List<string>()
					{
						"政治", "经济", "军事", "教育"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "查看一个图标所表示的文件类型、位置、大小等，可使用右键菜单的(        )命令。", Option = new List<string>()
					{
						"打开", "快速查看", "重命名", "属性"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在“我的电脑”和“资源管理器”中，以下对文件和文件夹的操作结果的描述中，错误的是(        )。", Option = new List<string>()
					{
						"移动文件后，文件从原来的文件夹中消失，在目标文件夹中出现", "复制文件后，文件仍保留在原来的文件夹中，在目标文件夹中出现该文件的复制品", "删除文件或文件夹后，被删除的内容被放入回收站", "选中多个文件或文件夹后，只有没被选中的文件仍保留在磁盘上"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "查看C盘总容量、已用空间和可用空间，可在“我的电脑”或“资源管理器”中使用鼠标右键单击驱动器“（C:）”后，再(        )。", Option = new List<string>()
					{
						"单击“资源管理器”", "单击“打开”", "单击“查找", "单击“属性”"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在计算机启动后能管理计算机的所有资源。", Option = new List<string>()
					{
						"软件", "操作者", "应用程序", "操作系统"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "电子计算机中的信息是用(        )表示。", Option = new List<string>()
					{
						"ASCII码", "英文", "拼音", "二进制代码"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "操作系统是对（    ）进行管理的系统软件。", Option = new List<string>()
					{
						"硬件", "软件", "计算机资源", "应用程序"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "系统启动以后，操作系统常驻（   ）。", Option = new List<string>()
					{
						"硬盘", "软盘", "RAM", "ROM"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "按ASCII码值比较大小，下面正确的是（    ）。", Option = new List<string>()
					{
						"数字比字母大", "0比9大", "数字比字母小", "以上均错"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "微机中的内存储器（RAM)的功能是（    ）。", Option = new List<string>()
					{
						"仅存贮数据", "输出数据", "存贮程序和数据", "输入数据"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "微机中的中央处理器（CPU)的功能是（    ）。", Option = new List<string>()
					{
						"存储数据", "输出数据", "进行运算和控制", "输入数据"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "微机中，CAM的含义是（     ）。", Option = new List<string>()
					{
						"计算机辅助设计", "计算机辅助教学", "计算机辅助制造", "计算机辅助测量"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "使用（    ）可以实现全角和半角输入的状态切换。", Option = new List<string>()
					{
						"Ctrl+Shift", "Ctrl+Space", "Shift+Space", "Alt+Space"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在计算机中，用来存储和处理汉字用到汉字的（      ）。", Option = new List<string>()
					{
						"国标码", "输入码", "内码", "外码"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "五笔字形输入码属于（     ）。", Option = new List<string>()
					{
						"数字类", "字音类", "字形类", "混合类"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在下列存储器中，访问速度最快的是（    ）。", Option = new List<string>()
					{
						"磁盘存储器", "软盘存储器", "内存储器", "磁带存储器"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "磁盘是一种（    ）。", Option = new List<string>()
					{
						"输入设备", "输出设备", "输入输出设备", "打印设备"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在计算机中构成一个字节的二进制位是（   ）。", Option = new List<string>()
					{
						"2位", "4位", "8位", "16位"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在计算机的内部，用来传送、存储、加工处理的信息表示形式是（     ）。", Option = new List<string>()
					{
						"ASCII码", "拼音简码", "二进制码", "八进制码"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中，要事先查看当前文档的打印效果，应进行(        )。", Option = new List<string>()
					{
						"页面设置", "打印", "打印预览", "全屏显示"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中，要删除当前选定的文本并将其放在剪贴板上的操作是(        )。", Option = new List<string>()
					{
						"清除", "复制", "剪切", "粘贴"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在FrontPage窗口中，单击“文件”菜单中的(        )选项，可以建立新网页。", Option = new List<string>()
					{
						"打开", "保存", "新建", "关闭"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "目前网络传输介质中传输速率最高的是(        )。", Option = new List<string>()
					{
						"双绞线", "同轴电缆", "光缆", "电话线"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "电子计算机的硬件系统基本由(        )五个部分组成。", Option = new List<string>()
					{
						"输入设备、内存储器、外存储器、运算器、输出设备", "输入设备、内存储器、外存储器、控制器、输出设备", "输入设备、存储器、运算器、控制器、输出设备", "输入设备、存储器、运算器、CPU、输出设备"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "十进制数68转换为二进制数是(        )。", Option = new List<string>()
					{
						"“1000101”", "“1001000”", "“1000100”", "“1001010”"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "以下文件类型中，(        )不是声音文件类型。", Option = new List<string>()
					{
						"WAV", "MID", "AVI", "MP3"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "以下文件类型中(        )是经过“有损压缩”，以损失图片质量达到文件占用空间减少的图片类型。", Option = new List<string>()
					{
						"BMP", "GIF", "JPG", "PSD"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "WWW中的每个站点都有唯一的站点地址，叫(        )（即统一资源定位器）。", Option = new List<string>()
					{
						"http", "WWW", "URL", "Web"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "以下关于电子邮件的说法不正确的是(        )。", Option = new List<string>()
					{
						"电子邮件的英文简称是E-mail.", "加入因特网的每个用户通过申请都可以得到一个“电子信箱”", "在一台计算机上申请的“电子信箱”，以后只有通过这台计算机上网才能收信", "一个人可以申请多个电子信箱"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "以下设备中，不属于输出设备的是(        )。", Option = new List<string>()
					{
						"显示器", "打印机", "扫描仪", "绘图仪"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "常用的3.5英寸软盘的容量是(        )。", Option = new List<string>()
					{
						"720KB", "1.2MB", "1.44MB", "1GB"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "(        )是存储器最基本的存储单位。", Option = new List<string>()
					{
						"位", "字长", "字节", "字符"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "如何设定计算机的开机密码？", Option = new List<string>()
					{
						"找系统管理员", "在控制面板上设定", "在bios中设定", "在系统中设定"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "计算机诞生的时间？", Option = new List<string>()
					{
						"1984", "1978", "1946", "1964"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中如何插入特殊符号？", Option = new List<string>()
					{
						"鼠标输入", "键盘输入", "通过插入菜单中的符号插入", "通过插入菜单中的图片插入"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "对软盘存放环境的温度要求应为（     ）。", Option = new List<string>()
					{
						"0℃--35℃", "4℃--50℃", "20℃--22℃", "14℃--40℃"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "对软盘存放环境的湿度要求应为（    ）。", Option = new List<string>()
					{
						"5％--60％", "8％--83％", "40％--60％", "14％--40％"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "16*16点阵字型码汉字，一个汉字占（    ）的存储空间。", Option = new List<string>()
					{
						"16字节", "32字节", "48字节", "64字节"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "拼音输入法属于汉字编码中的（    ）。", Option = new List<string>()
					{
						"内码", "外码", "字型码", "ASCII码"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "计算机的工作环境温度为（    ）。", Option = new List<string>()
					{
						"0度～50度", "15度～30度", "15度～50度", "-15度～15度"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列都属于硬件的选项是（   ）。", Option = new List<string>()
					{
						"CPU、ROM和DOS", "软盘、硬盘和光盘", "鼠标、WPS和RAM", "ROM、RAM和PASCAL"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "目前的光驱数据传输速率在40倍速以上，数据传输率是光驱的技术指标之一，数据传输率(  )被定为一倍速。", Option = new List<string>()
					{
						"50KB/s", "150KB/s", "150Kb/s", "50Kb/s"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "软盘的存放环境，磁场的强度应小于（     ）奥斯特。", Option = new List<string>()
					{
						"20", "50", "70", "90"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "微机中，CAI的含义是（    ）。", Option = new List<string>()
					{
						"计算机辅助设计", "计算机辅助教学", "计算机辅助制造", "计算机辅助测量"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在计算机存储器中，存储英文字母\"A\"，存储的是它的（    ）。", Option = new List<string>()
					{
						"输入码", "ASCII", "输出码", "字形码"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "使用（    ）可以实现中英文输入的快速切换。", Option = new List<string>()
					{
						"Ctrl+Shift", "Ctrl+Space", "Shift+Space", "Alt+Space"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "国标码是把区位码中的区码和位码变为十六进制后，再分别加上（      ）。", Option = new List<string>()
					{
						"10H", "20H", "30H", "40H"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "为了将汉字输入到计算机中而编制的代码叫汉字（   ）。", Option = new List<string>()
					{
						"国标码", "输入码", "内码", "输出码"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "区位码输入法属于（    ）。", Option = new List<string>()
					{
						"字音类", "数字类", "字形类", "混合类"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列设备中不能作为输出设备的（     ）。", Option = new List<string>()
					{
						"打印机", "键盘", "显示器", "绘图仪"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "微机中，RAM是（     ）。", Option = new List<string>()
					{
						"只读存储器", "随机存储器", "硬盘", "光盘"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在下列设备中，属于输入设备的是（    ）。", Option = new List<string>()
					{
						"显示器", "键盘", "CD-ROM", "打印机"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "中央处理器主要由（    ）构成。", Option = new List<string>()
					{
						"硬件和软件", "控制器和运算器", "主机和外设", "控制器和存储器"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Pentium是64位机，这里的64位表示（    ）。", Option = new List<string>()
					{
						"字节", "字长", "容量", "速度"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word编辑状态，当前编辑文档中的字体全是宋体，选中一段文字设为楷体，则(        )。", Option = new List<string>()
					{
						"文档全文的字体都是楷体", "被选中文字的字体变为楷体", "被选中文字的字体仍为宋体", "文档全文的字体不变"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中，设置文字为斜体应使用格式工具栏中的(        )。", Option = new List<string>()
					{
						"B 按钮", "I按钮", "U按钮", "A按钮"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "如果想保存你感兴趣的网页地址，可以使用IE浏览器中的(        )。", Option = new List<string>()
					{
						"“历史”按钮", "“收藏”菜单", "“搜索”按钮", "“编辑”菜单"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "搜索引擎可以用来(        )。", Option = new List<string>()
					{
						"收发电子邮件", "检索网络信息", "拔打网络电话", "发布信息"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "所有E-mail地址的通用格式是(        )。", Option = new List<string>()
					{
						"邮件服务器名@用户名", "用户名@邮件服务器名", "用户名#邮件服务器名", "邮件服务器名#用户名"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "希望从因特网下载游戏、学术论文等信息资源，可以通过(        )服务下载。", Option = new List<string>()
					{
						"E-mail", "WWW", "BBS", "FTP"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "因特网使用的网络协议是(        )协议。", Option = new List<string>()
					{
						"IPX/SPX", "TCP/IP", "HTTP", "ISDN"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "计算机程序的三种结构是顺序结构、(        )、选择结构。", Option = new List<string>()
					{
						"模块结构", "循环结构", "多重循环结构", "块IF结构"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "关闭运行Windows 98操作系统的计算机，正确的方法是(        )。", Option = new List<string>()
					{
						"直接关闭计算机电源", "执行：开始→关闭系统→关闭计算机", "执行：开始→关闭系统→将您的计算机转入睡眠状态→关闭计算机电源", "执行：开始→关闭系统→重新启动计算机并切换到MS-DOS方式"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "是Windows 98为挽救那些被删除之后又可能需要找回来的文件所设置的。", Option = new List<string>()
					{
						"收藏夹", "回收站", "我的公文包", "桌面"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "当Windows系统出现某些错误而不能正常启动或运行时，为了提高系统自身的安全性，在启动时可以进入(        )模式。", Option = new List<string>()
					{
						"正常启动", "安全", "命令提示符", "单步启动"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在“我的电脑”或“资源管理器”中使用鼠标同时选中多个不连续的文件时，需要按住(        )。", Option = new List<string>()
					{
						"Shift键", "Ctrl键", "Alt键", "Caps Lock键"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "屏幕分辨率是指屏幕区域由多少个(        )组成。", Option = new List<string>()
					{
						"线条", "像素点", "图标", "颜色"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "最重要的系统软件是(        )。", Option = new List<string>()
					{
						"数据库", "操作系统", "因特网", "电子邮件"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "是随机存储器的缩写，这种存储器是一种(        )存储器。", Option = new List<string>()
					{
						"RAM、只读", "RAM、读写", "ROM、只读", "ROM、读写"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列四项中不是文件属性的是（   ）。", Option = new List<string>()
					{
						"文件", "隐藏", "系统", "只读"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "用不同的汉字输入方法输入汉字后，该汉字的内码是（     ）。", Option = new List<string>()
					{
						"相同的", "完全不相同", "大部分相同", "部分相同"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "汉字的字型码（又称字模）是存放在（    ）。", Option = new List<string>()
					{
						"汉字库文件中", "键盘管理程序中", "汉字系统启动程序中", "显示管理程序中"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "若要显示或打印汉字，将用到汉字编码中的（    ）。", Option = new List<string>()
					{
						"字型码", "输入码", "机内码", "交换码"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "对一个文件来说，必须有（     ）。", Option = new List<string>()
					{
						"文件主名", "文件扩展名", "文件连接符", "文件分隔符"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "计算机工作环境的相对湿度最好在（     ）。", Option = new List<string>()
					{
						"40%～60%", "10%～80%", "0%～100%", "20%～40%"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "常用的CD-ROM光盘能进行的操作是（    ）。", Option = new List<string>()
					{
						"读", "写", "读/写", "删除"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "比较两个英文字符串的大小的方法是（   ）。", Option = new List<string>()
					{
						"从第一个字符比起，比其ASCII值，第一个相同再比第二个，逐个向后比", "从最后一个字符比起，比其ASCII值，逐个向前比", "从第一个字符比起，以ASCII码值的逆序，逐个向后比", "从最后一个字符比起，以ASCII码值的逆序，逐个向前比"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "微机运行程序时需要占用内存，这里所说的内存是指（    ）。", Option = new List<string>()
					{
						"RAM", "ROM", "硬盘", "软盘"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列说法中，（    ）是正确的。", Option = new List<string>()
					{
						"软盘的数据存储量远比硬盘小", "可以把好几张磁盘软盘合并成一个磁盘组", "软盘的体积比硬盘大", "读取硬盘上的数据所需的时间较软盘多"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "微机系统与外部交换信息主要通过（    ）。", Option = new List<string>()
					{
						"I/O设备", "键盘", "光盘", "内存"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "字母\"A\"的ASCII码是十进制数65，字母\"B\"的ASCII码是十进制数（    ）。", Option = new List<string>()
					{
						"66", "67", "97", "1"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "正确插入软盘的方法是", Option = new List<string>()
					{
						"标签向上，方孔朝外", "标签向下，方空朝里", "标签向下，方空朝外", "标签向下，方空朝里"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "微机中，CAD的含义是（     ）。", Option = new List<string>()
					{
						"计算机辅助设计", "计算机辅助教学", "计算机辅助制造", "计算机辅助测量"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "区位码是应用与汉字处理过程中（    ）环节的汉字编码。", Option = new List<string>()
					{
						"输入", "交换", "存储", "输出"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "使用（    ）可以在系统提供的汉字输入方法之间切换。", Option = new List<string>()
					{
						"Ctrl+Shift", "Ctrl+Space", "Shift+Space", "Alt+Space"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "防病毒软件和防病毒卡相比，（    ）是防病毒软件的特点之一。", Option = new List<string>()
					{
						"便于升级", "不便于升级", "成本高", "速度快"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "对明确标有上、下两个符号的键，如欲输入上档符号应使用（    ）。", Option = new List<string>()
					{
						"Shift+某键", "Ctrl+某键", "Alt+某键", "Del+某键"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在计算机领域中，媒体是指（    ）。", Option = new List<string>()
					{
						"表示和传播信息的载体", "各种信息的编码", "计算机的输入和输出信息", "计算机屏幕显示的信息"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "微机中，ROM是（    ）。", Option = new List<string>()
					{
						"只读存储器", "随机存储器", "硬盘", "光盘"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "掉电之后，信息全部丢失的是（    ）。", Option = new List<string>()
					{
						"RAM", "ROM", "软盘", "硬盘"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "内存的存储容量为32MB的含义是（    ）。", Option = new List<string>()
					{
						"32*1024*1024个字节", "32*1024个字节", "32*1048567个二进制位", "32千个二进制位"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "微机中1KB的含义是（    ）。", Option = new List<string>()
					{
						"1024字节", "1000字节", "1000个二进制位", "1个字节"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "计算机网络的主要功能之一是（    ）。", Option = new List<string>()
					{
						"资源共享", "通存通兑", "会计电算化", "保存数据"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "一个完整的计算机系统是由（    ）组成的。", Option = new List<string>()
					{
						"计算机的软件系统和硬件系统", "键盘和显示器", "系统软件和应用软件", "内存和外设"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "根据冯.诺依曼原理，计算机硬件的基本组成是（   ）。", Option = new List<string>()
					{
						"输入设备、输出设备、运算器、控制器、存储器", "磁盘、软盘、内存、CPU、显示器", "打印机、触摸屏、键盘、软盘", "鼠标、打印机、主机、显示器、存储器"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word编辑状态下，要删除光标右边的文字，应使用(        )。", Option = new List<string>()
					{
						"Delete键", "Ctrl键", "BackSpace键", "Alt键"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "FrontPage Express软件是(        )。", Option = new List<string>()
					{
						"网页制作软件", "文字处理软件", "电子表格软件", "演示文稿制作软件"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "我国从1994年起，开通了Internet的全功能服务。根据国务院的规定，有权直接与国际Internet连接的网络有(        )个。", Option = new List<string>()
					{
						"3", "4", "5", "6"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "网络的域名地址中，有一部分代表国家，其中(        )代表中国。", Option = new List<string>()
					{
						"CH", "CN", "ZH", "ZG"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "浏览网页使用的软件叫做(        )。", Option = new List<string>()
					{
						"浏览器", "网站", "电子邮箱", "WinZip"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "要制作网页，就要使用网页制作工具如(        )。", Option = new List<string>()
					{
						"FrontPage", "Excel", "Word", "PowerPoint"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "使用电话拨号连接因特网时需要Modem，其中文名称是(        )。", Option = new List<string>()
					{
						"调制解调器", "网卡", "麦克风", "浏览器"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "人类赖以生存和利用的三项战略资源是物质、能量、(        )。", Option = new List<string>()
					{
						"信息", "信息技术", "电脑", "因特网"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "网络黑客为了非法闯入一个网络系统，(        )是黑客们攻击的主要目标。", Option = new List<string>()
					{
						"口令", "电子邮件", "病毒", "WWW网址"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在(        )情况下软盘必须进行完全格式化才能使用。", Option = new List<string>()
					{
						"新的且没有经过格式化", "软盘有部分损坏", "软盘上有被病毒感染的文件", "软盘上存有不再需要的文件"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列菜单命令中，(        )属于“文件”菜单。", Option = new List<string>()
					{
						"新建、打开、保存", "帮助主题", "剪切、复制、粘贴", "工具栏"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Windows 98的任务栏上通常有一个(        )栏，栏内一般有4个图标：查看频道、启动Internet Explorer、启动Outlook Express及显示桌面。", Option = new List<string>()
					{
						"快速启动", "地址", "链接", "工具"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "当屏幕上鼠标的指针变为沙漏图型时，你应该(        )。", Option = new List<string>()
					{
						"等待", "继续单击或双击鼠标", "热启动", "按回车键"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows屏幕最下面写着“开始”的长条形栏叫(        )。", Option = new List<string>()
					{
						"任务栏", "菜单栏", "状态栏", "工具栏"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "将软盘写保护后能(        )文件。", Option = new List<string>()
					{
						"读", "写", "读/写", "修改"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "常用的CD-ROM光盘能进行(        )操作。", Option = new List<string>()
					{
						"读", "写", "读/写", "删除"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "计算机系统是由硬件系统和(        )组成。", Option = new List<string>()
					{
						"软件系统", "系统软件", "系统硬件", "应用软件"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列软件中，属于系统软件的是(        )。", Option = new List<string>()
					{
						"Windows", "Word", "WPS", "Excel"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "以下说法正确的是(        )。", Option = new List<string>()
					{
						"计算机语言有机器语言、汇编语言、高级语言", "计算机语言只有三种，即Basic语言、Pascal语言、C语言", "计算机语言只有三种，即Basic语言、Pascal语言、C语言", "高级语言接近自然语言，能被计算机直接识别和接受"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "现代的计算机系统都属于(        )体系。", Option = new List<string>()
					{
						"冯.诺依曼", "比尔.盖茨", "唐纳德.希斯", "温.瑟夫"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在字处理软件中如何选择所有文档内容？", Option = new List<string>()
					{
						"三击鼠标", "双击鼠标", "单击鼠标", "不能选择全部文档内容"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下面哪一个是计算机的内设？", Option = new List<string>()
					{
						"主板", "打印机", "扫描仪", "显示器"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "如何保存Word文件？", Option = new List<string>()
					{
						"在文件中保存", "新建按纽", "关闭", "插入菜单"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[0], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "程序存储到磁盘的形式是（   ）。", Option = new List<string>()
					{
						"字符", "字组", "记录", "文件"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98的整个显示屏幕称为（    ）。", Option = new List<string>()
					{
						"窗口", "屏幕", "工作台", "桌面"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列叙述中，正确的一条是（     ）。", Option = new List<string>()
					{
						"\"开始\"菜单只能用鼠标单击\"开始\"按钮才能打开", "Windows的任务栏的大小是不能改变的", "\"开始\"菜单是系统生成的，用户不能再设置它", "Windows的任务栏可以放在桌面的四个边的任意边上"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98中删除文件夹的方法之一是（    ）。", Option = new List<string>()
					{
						"鼠标左键单击该文件夹", "鼠标右键单击该文件夹", "鼠标左键双击该文件夹", "把该文件夹图标拖放到回收站图标上"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Windows98中，可以启动多个应用程序，通过（    ）在应用程序之间切换。", Option = new List<string>()
					{
						"资源管理器", "我的电脑", "程序菜单", "任务栏"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "按照Windows98中的文件命名规则，下列文件名（    ）为非法文件名。", Option = new List<string>()
					{
						"my file", "Basic Program", "card \"1\"", "class1.\\data"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "利用Windows98资源管理器不能完成的操作是（     ）。", Option = new List<string>()
					{
						"改变文件的属性", "复制文件", "为文件改名", "修改报告格式"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在资源管理器中，如果发生误操作将C盘文件删除，可以（   ）。", Option = new List<string>()
					{
						"在回收站对此文件执行\"还原\"操作", "从回收站将此文件托回原位置", "在资源管理器中执行\"撤销\"命令", "以上均可"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "回收站中可以是以下内容（     ）。", Option = new List<string>()
					{
						"文件", "文件夹", "快捷方式", "以上都对"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "用鼠标拖动功能实现文件或文件夹的快速移动时，正确的操作是（    ）。", Option = new List<string>()
					{
						"用鼠标左键拖动文件或文件夹到目标文件夹上", "用鼠标右键拖动文件或文件夹到目标文件夹上，然后在弹出的菜单中选择\"移动到当前位置\"", "按住Ctrl键，然后用鼠标左键拖动文件或文件夹到目标文件夹上", "按住Shift键，然后用鼠标左键拖动文件或文件夹到目标文件夹上"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "查看一个图标所表示的文件类型、位置、大小等，可使用右键菜单的(        )命令。", Option = new List<string>()
					{
						"打开", "快速查看", "重命名", "属性"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在“我的电脑”和“资源管理器”中，以下对文件和文件夹的操作结果的描述中，错误的是(        )。", Option = new List<string>()
					{
						"移动文件后，文件从原来的文件夹中消失，在目标文件夹中出现", "复制文件后，文件仍保留在原来的文件夹中，在目标文件夹中出现该文件的复制品", "删除文件或文件夹后，被删除的内容被放入回收站", "选中多个文件或文件夹后，只有没被选中的文件仍保留在磁盘上"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "查看C盘总容量、已用空间和可用空间，可在“我的电脑”或“资源管理器”中使用鼠标右键单击驱动器“（C:）”后，再(        )。", Option = new List<string>()
					{
						"单击“资源管理器”", "单击“打开”", "单击“查找", "单击“属性”"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98中，\"粘贴\"的快捷键是（    ）。", Option = new List<string>()
					{
						"Ctrl+A", "Ctrl+Space", "Ctrl+V", "Ctrl+C"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列文件名中，（    ）是非法的Windows98文件名。", Option = new List<string>()
					{
						"This is my file", "关于改进服务的通告", "*帮助信息*", "student,dbf"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98支持长文件名，一个文件名的最大长度可达（    ）个字符。", Option = new List<string>()
					{
						"225", "256", "255", "128"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下面关于文件夹的命名的说法中不正确的是（    ）。", Option = new List<string>()
					{
						"可以使用长文件名", "可以包含空格", "其中可以包含\"？\"", "其中不能包含\"<\""
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Windows中将信息传送到剪切板不正确的方法是（     ）。", Option = new List<string>()
					{
						"用\"复制\"命令把选定的对象送到剪切板", "用\"剪切\"命令把选定的对象送到剪切板", "用Ctrl+V把选定的对象送到剪切板", "Alt+PrintScreen把当前窗口送到剪切板"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98是一个（      ）。", Option = new List<string>()
					{
						"多用户多任务操作系统", "单用户单任务操作系统", "单用户多任务操作系统", "多用户分时操作系统"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Windows98环境中，对磁盘文件进行管理的一个常用工具是（       ）。", Option = new List<string>()
					{
						"写字板", "我的公文包", "资源管理器", "剪贴板"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98是多任务操作系统，所谓\"多任务\"的含义是（    ）。", Option = new List<string>()
					{
						"可以同时复制多个文件", "可以同时移动多个文件", "可以同时运行多个应用程序", "可以允许多个用户同时使用"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Windows环境中，鼠标是重要的输入工具，而键盘（    ）。", Option = new List<string>()
					{
						"无法起作用", "只能在输入汉字时起作用", "也能完成几乎所有操作", "只能在菜单操作中使用"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在资源管理器中，文件夹树中的某个文件夹图标左边的\"+\"表示（    ）。", Option = new List<string>()
					{
						"该文件夹含有隐藏文件", "文件夹为空", "该文件夹含有子文件夹", "该文件夹含有系统文件"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98启动时自动执行磁盘扫描程序，可能是由于（     ）。", Option = new List<string>()
					{
						"磁盘坏了", "上一次使用计算机时，Word文档没有存盘", "上一次使用计算机时，非正常关机", "非法用户正在开机"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "磁盘上的根文件夹是（    ）。", Option = new List<string>()
					{
						"用户建立的", "自动存在的", "根本不存在", "以上都错"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "已经选定文件夹后，下列操作中不能删除该文件夹的操作是（    ）。", Option = new List<string>()
					{
						"在键盘上按Del键", "用鼠标左键双击该文件夹", "用鼠标右键单击该文件夹，打开快捷菜单，然后选择\"删除\"命令", "在文件菜单中选择\"删除\"命令"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在选定文件或文件夹后，下列的（    ）操作不能修改文件或文件夹的名称。", Option = new List<string>()
					{
						"在\"文件\"菜单中选择\"重命名\"命令，然后键入新文件名再按回车", "鼠标左键双击要改名的文件", "用鼠标左键单击文件或文件夹的名称，然后键入新文件名再回车", "用鼠标右键单击文件或文件夹的图标，选择\"重命名\"，然后键入新文件名再回车"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "再Windows资源管理器中，在按下Shift键的同时执行删除某文件的操作是（    ）。", Option = new List<string>()
					{
						"将文件放入回收站", "将文件直接删除", "将文件放入上一层文件夹", "将文件放入下一层文件夹"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "按组合键（    ）可以打开\"开始\"菜单。", Option = new List<string>()
					{
						"<Ctrl>+<O>", "<Ctrl>+<Esc>", "<Ctrl>+<空格键>", "<Ctrl>+<Tab>"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98中的回收站是（     ）。", Option = new List<string>()
					{
						"一个内存区域", "硬盘上的一个区域", "软盘上的一个区域", "高速缓存中的一个区域"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98中对话框与窗口的主要区别之一是（    ）。", Option = new List<string>()
					{
						"窗口不能改变大小", "对话框不能改变大小", "对话框不能关闭", "用户不能使用"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98中，一个菜单命令后面带有符合\"...\"，表示点击该命令后会（   ）。", Option = new List<string>()
					{
						"弹出下级菜单", "弹出对话框", "退出当前窗口", "以上均错"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "通常\"任务栏\"中的按钮代表（    ）。", Option = new List<string>()
					{
						"一个可执行程序", "一个正在执行的程序", "一个关闭的程序窗口", "一个不工作的程序窗口"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98中文件夹的组织结构是一种（   ）。", Option = new List<string>()
					{
						"表格结构", "树形结构", "网状结构", "线形结构"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "关闭运行Windows 98操作系统的计算机，正确的方法是(        )。", Option = new List<string>()
					{
						"直接关闭计算机电源", "执行：开始→关闭系统→关闭计算机", "执行：开始→关闭系统→将您的计算机转入睡眠状态→关闭计算机电源", "执行：开始→关闭系统→重新启动计算机并切换到MS-DOS方式"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "是Windows 98为挽救那些被删除之后又可能需要找回来的文件所设置的。", Option = new List<string>()
					{
						"收藏夹", "回收站", "我的公文包", "桌面"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "当Windows系统出现某些错误而不能正常启动或运行时，为了提高系统自身的安全性，在启动时可以进入(        )模式。", Option = new List<string>()
					{
						"正常启动", "安全", "命令提示符", "单步启动"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在“我的电脑”或“资源管理器”中使用鼠标同时选中多个不连续的文件时，需要按住(        )。", Option = new List<string>()
					{
						"Shift键", "Ctrl键", "Alt键", "Caps Lock键"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98中，一组菜单命令中有一个前面带有符号\"●\"，表示这是一组（   ）。", Option = new List<string>()
					{
						"单选命令", "多选命令", "当前无效命令", "非法命令"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "关闭一台运行Windows98的计算机之前应先（     ）。", Option = new List<string>()
					{
						"关闭所有已打开的程序", "关闭Windows98", "断开服务器连接", "关闭主机电源"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "\"开始\"按钮，通常位于桌面的（     ）。", Option = new List<string>()
					{
						"底行左侧", "底行右侧", "左上侧", "右上侧"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "对一个文件夹来说，必须有（    ）。", Option = new List<string>()
					{
						"文件主名", "文件扩展名", "文件连接符", "文件分隔符"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows98的菜单中某菜单命令是灰色，这表示（     ）。", Option = new List<string>()
					{
						"该菜单命令暂时无效", "该菜单命令当前有效", "该菜单命令已被选中", "该菜单命令有下级子菜单"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Windows98中，按下鼠标左键在不同驱动器的文件夹之间拖动某一文件后，其结果是（     ）。", Option = new List<string>()
					{
						"复制该文件", "移动该文件", "删除该文件", "无任何结果"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Windows98中，应用程序启动后驻留在（     ）。", Option = new List<string>()
					{
						"内存", "外存", "我的电脑", "剪贴板"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列菜单命令中，(        )属于“文件”菜单。", Option = new List<string>()
					{
						"新建、打开、保存", "帮助主题", "剪切、复制、粘贴", "工具栏"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Windows 98的任务栏上通常有一个(        )栏，栏内一般有4个图标：查看频道、启动Internet Explorer、启动Outlook Express及显示桌面。", Option = new List<string>()
					{
						"快速启动", "地址", "链接", "工具"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "当屏幕上鼠标的指针变为沙漏图型时，你应该（        ）。", Option = new List<string>()
					{
						"等待", "继续单击或双击鼠标", "热启动", "按回车键"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Windows屏幕最下面写着“开始”的长条形栏叫(        )。", Option = new List<string>()
					{
						"任务栏", "菜单栏", "状态栏", "工具栏"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[1], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "启动Word后可以新建文档的个数是（    ）。", Option = new List<string>()
					{
						"1个", "4个", "6个", "受内存空间的限制"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Word97能处理（    ）种文件类型。", Option = new List<string>()
					{
						"1", "2", "3", "多种"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Word程序启动后就自动打开一个名为（     ）的文档。", Option = new List<string>()
					{
						"Noname", "Untitled", "文件1", "文档1"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中，将当前文档换名保存时，正确的操作是(        )。", Option = new List<string>()
					{
						"单击常用工具栏上的“保存”按钮", "选择“编辑”菜单中的“复制”命令", "选择“文件”菜单中的“保存”命令", "选择“文件”菜单中的“另存为…”命令"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中，选择一个字符后，连击两次工具条中的斜体\"I\"按钮，则该字符的格式（    ）。", Option = new List<string>()
					{
						"是左斜体", "是右斜体", "没有变化", "出错"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在\"字体颜色\"调色板中，系统默认的颜色设置为（   ）。", Option = new List<string>()
					{
						"黑色", "白色", "自动", "无"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "工具栏中的\"Ｂ\"表示（    ）。", Option = new List<string>()
					{
						"字母b的大写", "字体颜色为黑色", "加粗", "打印"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "进入扩展选取模式的快捷键是(     )。", Option = new List<string>()
					{
						"F6", "F7", "F8", "F9"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word97中，用鼠标在文档选定中连续快速击打三次，其作用是（     ）。", Option = new List<string>()
					{
						"选择一行文本", "选择一段文本", "选择整个文本", "没有任何变化"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "退出Word程序的快捷键是（     ）。", Option = new List<string>()
					{
						"Ctrl+S", "Alt+S", "Alt+F4", "Ctrl+F4"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Word97的\"文件\"菜单下部一般列出4个用户最近所用过的文档名，此文档名的个数最多可设置为（    ）。", Option = new List<string>()
					{
						"6", "8", "9", "12"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "退出Word97的正确操作是（     ）。", Option = new List<string>()
					{
						"单击\"文件\"菜单中的\"关闭\"命令", "单击文档窗口上的关闭窗口按钮×", "单击\"文件\"菜单中的\"退出\"命令", "单击Word窗口的最小化按钮_"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word97中，下列不能打印输出当前编辑的文档的操作是（     ）。", Option = new List<string>()
					{
						"单击\"文件\"菜单下的\"打印\"选项", "单击常用工具栏中的\"打印\"按钮", "单击\"文件\"菜单下的\"页面设置\"选项", "单击\"文件\"菜单下的\"打印预览\"选项，再单击工具栏中的\"打印\"按钮"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Word97中，在（    ）视图下可以插入页眉和页脚。", Option = new List<string>()
					{
						"普通", "大纲", "页面", "主控文档"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Word97中有（    ）种视图。", Option = new List<string>()
					{
						"1", "3", "5", "7"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "要将Word文档中一部分选定的文字移动到指定的位置，首先对它进行的操作是（     ）。", Option = new List<string>()
					{
						"单击\"编辑\"菜单下的\"复制\"命令", "单击\"编辑\"菜单下的\"清除\"命令", "单击\"编辑\"菜单下的\"剪切\"命令", "单击\"编辑\"菜单下的\"粘贴\"命令"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中，要事先查看当前文档的打印效果，应进行(        )。", Option = new List<string>()
					{
						"页面设置", "打印", "打印预览", "全屏显示"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中，要删除当前选定的文本并将其放在剪贴板上的操作是(        )。", Option = new List<string>()
					{
						"清除", "复制", "剪切", "粘贴"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中如何插入特殊符号？", Option = new List<string>()
					{
						"鼠标输入", "键盘输入", "通过插入菜单中的符号插入", "通过插入菜单中的图片插入"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中，可以同时显示水平标尺和垂直标尺的视图方式是（     ）。", Option = new List<string>()
					{
						"普通视图", "页面视图", "大纲视图", "全屏幕显示"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中录入中内容时，系统是否可以自动向文档添置页面分隔符。", Option = new List<string>()
					{
						"不一定", "可以", "不可以", "根据实际情况才能决定"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "字符排版内容不包括（     ）。", Option = new List<string>()
					{
						"定义字体", "定义文档位置", "定义字符修饰效果", "定义字符颜色"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "以下（   ）文件是Word文件。", Option = new List<string>()
					{
						"ABC.BMP", "ABZ.DOC", "SDF.DBF", "TT.BAT"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Word的主要功能是（     ）。", Option = new List<string>()
					{
						"电子表格", "字处理", "图形处理", "数字处理"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中，用鼠标选定一个矩形区域的文字时，需按住（  ）键。", Option = new List<string>()
					{
						"Ctrl", "Alt", "Del", "Space"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word97中，用鼠标在文档选定中连续快速击打三次，其作用与（     ）等同。", Option = new List<string>()
					{
						"Ctrl+S", "Ctrl+A", "Ctrl+P", "Ctrl+O"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "（     ）位于标题栏之下，它包含全部Word命令。", Option = new List<string>()
					{
						"工具栏", "菜单栏", "工作区", "状态栏"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word编辑状态，当前编辑文档中的字体全是宋体，选中一段文字设为楷体，则(        )。", Option = new List<string>()
					{
						"文档全文的字体都是楷体", "被选中文字的字体变为楷体", "被选中文字的字体仍为宋体", "文档全文的字体不变"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中，设置文字为斜体应使用格式工具栏中的(        )。", Option = new List<string>()
					{
						"B 按钮", "I按钮", "U按钮", "A按钮"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word编辑状态下，操作对象经常是被选择的内容，若鼠标在某行行首的左边，下列（     ）操作可以仅选择光标所在的行。", Option = new List<string>()
					{
						"单击鼠标左键", "将鼠标左键击三下", "双击鼠标左键", "单击鼠标右键"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word编辑状态下，当前输入的文字显示在（     ）。", Option = new List<string>()
					{
						"插入点", "鼠标光标处", "文件尾部", "当前行尾部"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word中，用鼠标选定一个矩形区域的文字时，需按住（     ）键。", Option = new List<string>()
					{
						"Alt", "Shift", "Enter", "Ctrl"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word环境中可以同时打开多个文档，但只有一个是当前活动文档，在这些文档窗口之间切换的命令菜是（    ）。", Option = new List<string>()
					{
						"窗口", "编辑", "文件", "工具"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Word是Windows下的（    ）。", Option = new List<string>()
					{
						"应用软件", "系统软件", "高级语言", "编译程序"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在\"字符间距\"选项卡中，系统默认的间距是（    ）。", Option = new List<string>()
					{
						"标准", "10磅", "20磅", "30磅"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "插入点符号\"│\"在文本区表示（     ）。", Option = new List<string>()
					{
						"当前文本输入或编辑的位置", "标识一个段落的结束", "标识文字大小", "选择输入法"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "退出扩展选取模式的快捷键是(     )。", Option = new List<string>()
					{
						"Esc", "F8", "Ctrl", "Alt"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在设置字号时，中文字号值越大，则实际显示的字将(    ).", Option = new List<string>()
					{
						"越小", "越大", "越粗", "越细"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Word中能否从文档中间开始打印（    ）。", Option = new List<string>()
					{
						"可以", "不可以", "转换成另类文件后可以", "有时可以，有时不可以"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "能否用标尺设置段落缩进（    ）。", Option = new List<string>()
					{
						"可以", "不可以", "根据文件大小而定", "根据文件属性而定"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word97中，同一计算机对同一个文档可以打开（     ）次。", Option = new List<string>()
					{
						"1", "2", "3", "不限"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "要对Word97文档的某一段落设置段落边界，首先应做的操作是（    ）。", Option = new List<string>()
					{
						"选定段落或将光标移动到此段落的任意处", "单击\"格式\"菜单的\"段落\"项", "单击\"格式\"菜单的\"边框与底纹\"项", "单击工具栏中的\"居中\"按钮"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "若要删除Word文档中一部分选定的文字的格式设置，可按组合键（     ）。", Option = new List<string>()
					{
						"Ctrl+Shift+Z", "Ctrl+Alt+Del", "Ctrl+F6", "Ctrl+Shift"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "要将Word文档中一部分选定的文字的中、英文字体，字形，字号，颜色等各项同时进行设置，应使用（     ）。", Option = new List<string>()
					{
						"\"格式\"菜单下的\"字体\"命令", "工具栏中的\"字体\"列表框选择字体", "\"工具\"菜单", "工具栏中的\"字号\"列表框选择字号"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Word编辑状态下，要删除光标右边的文字，应使用(        )。", Option = new List<string>()
					{
						"Delete键", "Ctrl键", "BackSpace键", "Alt键"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在字处理软件中如何选择所有文档内容？", Option = new List<string>()
					{
						"三击鼠标", "双击鼠标", "单击鼠标", "不能选择全部文档内容"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "如何保存Word文件？", Option = new List<string>()
					{
						"在文件中保存", "新建按纽", "关闭", "插入菜单"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[2], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在A2单元格内输入\"＝SUM(B3:C5,E7:G9)\"后按回车键，则A2最多存放（    ）个单元格内容的和。", Option = new List<string>()
					{
						"42", "6", "9", "15"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Excel97的一个工作簿文件最多可以包含（      ）个工作表。", Option = new List<string>()
					{
						"3", "30", "127", "255"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "按（      ）键活动单元格移到已使用过区域的右下角。", Option = new List<string>()
					{
						"PageDown", "Home", "Ctrl+Home", "Ctrl+End"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Excel工作簿中，当利用键盘对当前单元进行移动时，\"←\"表示（    ）。", Option = new List<string>()
					{
						"活动单元格左移一行", "活动单元格移动到表头", "活动单元格移动到表尾", "活动单元格左移一列"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在某工作表已经选定了一个连续的单元格区域后，用键盘和鼠标的组合键再选择一个单元格时，所用的键是（    ）。", Option = new List<string>()
					{
						"Shift", "Alt", "Space", "Ctrl"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "\"显示/隐藏工具栏\"应在（     ）菜单中操作。", Option = new List<string>()
					{
						"\"文件\"菜单", "\"工具\"菜单", "\"编辑\"菜单", "\"视图\"菜单"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Excel中选中一个区域，再单击键盘上的Del键，所完成的操作仅是（    ）。", Option = new List<string>()
					{
						"\"清除\"选中区域已设定的输出格式", "\"清除\"所有内容", "\"清除\"网格线", "\"清除\"选中区域的内容"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "将C3单元格的公式\"＝A2-$B3+C1\"复制到D4单元格，则D4单元格中的公式为（     ）。", Option = new List<string>()
					{
						"B3-$B4+D1", "B3-$C4+D2", "B2-$B4+D2", "B3-$B4+D2"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "某区域由A1,A2,A3,B1,B2,B3六个单元格组成，下列不能表示该区域的是（    ）。", Option = new List<string>()
					{
						"A1:B3", "A3:B1", "B3:A1", "A1:B1"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在某工作表中已经选定了一个连续的单元格区域后，用键盘和鼠标的组合操作再选另一个单元格时，所用的键是（     ）。", Option = new List<string>()
					{
						"Esc", "Shift", "Ctrl", "Alt"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "要将有数据且带有格式的单元格恢复为普通空单元格，先要选定该单元，然后使用（    ）。", Option = new List<string>()
					{
						"快捷菜单的\"删除\"命令", "Delete键", "\"编辑\"菜单的\"清除\"命令", "工具栏的\"剪切\"工具按钮"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "按（      ）键活动单元格移到A1单元。", Option = new List<string>()
					{
						"PageDown", "Home", "Ctrl+Home", "Ctrl+End"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "将鼠标指针指向某工作表标签，拖动标签到新位置，则完成（    ）操作。", Option = new List<string>()
					{
						"删除工作表", "复制工作表", "移动工作表", "重命名工作表"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在当前单元格中引用C5单元格地址，绝对地址引用是（    ）。", Option = new List<string>()
					{
						"C5", "$C5", "$C$5", "C$5"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "把Excel单元格指针移到Y100的最简单的方法是（      ）。", Option = new List<string>()
					{
						"拖动滚动条", "按Ctrl+Y100", "在名称框输入Y100", "先用Ctrl+→移到Y列，再用Ctrl+↓移到100行"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "日期1900-1-20在系统内部存储的是（      ）。", Option = new List<string>()
					{
						"1900-1-18", "1,20,1900", "20", "1900,1,20"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Excel单元格中输入数字字符串110111196812015673（身份证号码）时，应输入（     ）。", Option = new List<string>()
					{
						"1.10111196812015E+17", "\"110111196812015673\"", "1.10111196812015E+17", "110111196812015673‘"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Excel中输入字符串时，若该字符串的长度超过单元格的显示宽度，则超过部分最有可能（     ）。", Option = new List<string>()
					{
						"被截断删除", "继续超格显示", "给出错误提示", "作为另一个字符串存入右侧相邻单元"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在某单元格中公式的计算结果应为一个大于0的数，但却显示了错误信息\"＃＃＃＃＃\"，使用（   ）操作可以完全显示数据，且又簿影响该单元格的数据内容。", Option = new List<string>()
					{
						"加大该单元所在行的行高", "加大该单元所在列的列宽", "使用\"复制\"命令", "重新输入数据"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "将电话号码010-68320088输入单元格，可以键入（     ）。", Option = new List<string>()
					{
						"\"010-68320088", "010-68320088", "-68320078", "^010-68320088"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Excel97中要选定一张工作表，操作是（    ）。", Option = new List<string>()
					{
						"选\"窗口\"菜单中该工作簿的名称", "用鼠标单击该工作表的标签", "在名称框中输入该工作表的名称", "用鼠标将该工作表托放到最左边"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在缺省方式下，Excel97工作簿中的第一张工作表命名为（    ）。", Option = new List<string>()
					{
						"表1", "sheet1", "book1", "任意的表名"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "按（      ）键活动单元格移到当前行的最左侧。", Option = new List<string>()
					{
						"PageDown", "Home", "Ctrl+Home", "Ctrl+End"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Excel中输入字符串时，如该字符串的长度超过单元格的显示宽度，则超过部分最有可能（     ）。", Option = new List<string>()
					{
						"被截断删除", "继续超格显示", "给出错误信息", "作为另一个字符串存入右侧单元格"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "为了使打印出的工作表整体美观，在打印前应在（　　）中进行设置。", Option = new List<string>()
					{
						"\"文件\"菜单中的\"打印预览\"对话框", "\"文件\"菜单中的\"页面设置\"对话框", "\"文件\"菜单中的\"打印\"对话框", "\"视图\"菜单中的\"页眉和页脚\"对话框"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Excel中输入公式时必须以（　　）开头。", Option = new List<string>()
					{
						"\"", "＝", "\"", "-"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "将鼠标指针指向某工作表标签，按住Ctrl键拖动标签到新位置，则完成（    ）操作。", Option = new List<string>()
					{
						"删除工作表", "复制工作表", "移动工作表", "重命名工作表"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Excel工作簿文件的扩展名是（        ）。", Option = new List<string>()
					{
						"doc", "xls", "wps", "txt"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在打印工作表前就能看到实际打印效果的操作是（    ）。", Option = new List<string>()
					{
						"仔细观察工作表", "打印预览", "按F8键", "分页预览"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在Excel单元格中输入（      ），使该单元格显示0.3。", Option = new List<string>()
					{
						"2004-6-20", ".3", "\"6/20\"", "2004-6-20"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Excel工作表最多有（       ）列。", Option = new List<string>()
					{
						"65536", "256", "254", "128"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "使用鼠标托放方式\"填充\"数据时，鼠标的指针形状应该是（     ）。", Option = new List<string>()
					{
						"＋", "±", "？", "%"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在设置字号时，阿拉伯字号值（如12，14，16）越大，则实际显示的字将(    ).", Option = new List<string>()
					{
						"越大", "越小", "越粗", "越细"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下面（     ）是Excel文件。", Option = new List<string>()
					{
						"工资表.xls", "工资表.doc", "工资表.pps", "工资表.exe"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "单元地址\"B5\"表示（     ）。", Option = new List<string>()
					{
						"第B列与第5行的交汇处", "第B行与第5列的交汇处", "第B5行", "第B5列"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Excel工作表中最基本的单位是（　　）。", Option = new List<string>()
					{
						"单元格", "行", "列", "工作表"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "D5单元格中有公式\"＝A5+$B$4\",删除第3行后，D4中的公式是（      ）。", Option = new List<string>()
					{
						"A4+$B$3", "A4+$B$4", "A5+$B$3", "A5+$B$4"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在当前单元格中引用C5单元格地址，相对地址引用是（    ）。", Option = new List<string>()
					{
						"C5", "$C5", "$C$5", "C$5"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Excel工作表最多有（ ）行。<IMG border=undefined alt=\"\" src=\"http://localhost:8080/UpLoadFiles/20131027132315406.jpg\" onload=\"javascript:if(this.width>740)this.width=740\">", Option = new List<string>()
					{
						"65536", "256", "254", "128"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[3], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在FrontPage窗口中，单击“文件”菜单中的(        )选项，可以建立新网页。", Option = new List<string>()
					{
						"打开", "保存", "新建", "关闭"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[5], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "FrontPage Express软件是(        )。", Option = new List<string>()
					{
						"网页制作软件", "文字处理软件", "电子表格软件", "演示文稿制作软件"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[5], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列（    ）是Internet上政府的类别域名。", Option = new List<string>()
					{
						"edu", "ac", "org", "gov"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列（    ）是Internet澳洲的顶级域名。", Option = new List<string>()
					{
						"cn", "hk", "tw", "au"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "IP地址是一个32位的二进制数，被分为4部分，每部分由8位二进制数组成。通常人们把它写成4个十进制数，每个数的范围是0－255，彼此之间用（    ）分开。", Option = new List<string>()
					{
						";", ":", "\"", "."
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Internet所采用的通信协议是(        )。", Option = new List<string>()
					{
						"CSMA/CD", "X.25", "令牌环", "TCP/IP"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "目前家用拨号上网的调制解调器的速率没有(        )。", Option = new List<string>()
					{
						"28.8k", "33.6k", "56k", "128K"
					}, Answer	= 4, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "将不同区域或同一区域内的不同计算机连接起来的一种方式叫（      ）。", Option = new List<string>()
					{
						"客户机－服务器", "单用户", "网络", "多用户"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "代表中国的顶级域名是（    ）。", Option = new List<string>()
					{
						"com", "JP", "cn", "Telnet"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "目前网络传输介质中传输速率最高的是(        )。", Option = new List<string>()
					{
						"双绞线", "同轴电缆", "光缆", "电话线"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "WWW中的每个站点都有唯一的站点地址，叫(        )（即统一资源定位器）。", Option = new List<string>()
					{
						"http", "WWW", "URL", "Web"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "以下关于电子邮件的说法不正确的是(        )。", Option = new List<string>()
					{
						"电子邮件的英文简称是E-mail", "加入因特网的每个用户通过申请都可以得到一个“电子信箱”", "在一台计算机上申请的“电子信箱”，以后只有通过这台计算机上网才能收信", "一个人可以申请多个电子信箱"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "Internet最初创建的目的是用于(        )。", Option = new List<string>()
					{
						"政治", "经济", "军事", "教育"
					}, Answer	= 3, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列四项中，合法的IP地址是（    ）。", Option = new List<string>()
					{
						"190.220.5", "206.53.3.78", "206.53.312.78", "123,43,82,220"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "如果想保存你感兴趣的网页地址，可以使用IE浏览器中的(        )。", Option = new List<string>()
					{
						"“历史”按钮", "“收藏”菜单", "“搜索”按钮", "“编辑”菜单"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "搜索引擎可以用来(        )。", Option = new List<string>()
					{
						"收发电子邮件", "检索网络信息", "拔打网络电话", "发布信息"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "所有E-mail地址的通用格式是(        )。", Option = new List<string>()
					{
						"邮件服务器名@用户名", "用户名@邮件服务器名", "用户名#邮件服务器名", "邮件服务器名#用户名"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "希望从因特网下载游戏、学术论文等信息资源，可以通过(        )服务下载。", Option = new List<string>()
					{
						"E-mail", "WWW", "BBS", "FTP"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "因特网使用的网络协议是(        )协议。", Option = new List<string>()
					{
						"IPX/SPX", "TCP/IP", "HTTP", "ISDN"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "在E-mail地址中，@前面的是（    ）。", Option = new List<string>()
					{
						"用户名", "用户所在的国家", "计算机资源", "域名"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列哪个英文代表电子邮件（   ）。", Option = new List<string>()
					{
						"E-mail", "Veronica", "USENET", "Telnet"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列（    ）是Internet上\"科研机构\"的类别域名。", Option = new List<string>()
					{
						"ac", "com", "org", "net"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "下列（    ）是Internet加拿大的顶级域名。", Option = new List<string>()
					{
						"ca", "jp", "de", "au"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "我国从1994年起，开通了Internet的全功能服务。根据国务院的规定，有权直接与国际Internet连接的网络有(        )个。", Option = new List<string>()
					{
						"3", "4", "5", "6"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "网络的域名地址中，有一部分代表国家，其中(        )代表中国。", Option = new List<string>()
					{
						"CH", "CN", "ZH", "ZG"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "浏览网页使用的软件叫做(        )。", Option = new List<string>()
					{
						"浏览器", "网站", "电子邮箱", "WinZip"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "要制作网页，就要使用网页制作工具如(        )。", Option = new List<string>()
					{
						"FrontPage", "Excel", "Word", "PowerPoint"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "使用电话拨号连接因特网时需要Modem，其中文名称是(        )。", Option = new List<string>()
					{
						"调制解调器", "网卡", "麦克风", "浏览器"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "网络黑客为了非法闯入一个网络系统，(        )是黑客们攻击的主要目标。", Option = new List<string>()
					{
						"口令", "电子邮件", "病毒", "WWW网址"
					}, Answer	= 1, User = user, Verifier = verifier, Catalog = catalog[6], Points = 3
				},
				new AGPQuestionModel_SingleSelect()
				{
					Caption		= "计算机程序的三种结构是顺序结构、(        )、选择结构。", Option = new List<string>()
					{
						"模块结构", "循环结构", "多重循环结构", "块IF结构"
					}, Answer	= 2, User = user, Verifier = verifier, Catalog = catalog[7], Points = 3
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "对软盘进行写保护后，软盘中的数据（     ）。", Option = new List<string>()
					{
						"不能写也不能读", "可以写也可以读", "可以写但不能读", "可以读但不能写"
					}, Answer	= new List<int>()
					{
						4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "当一张软盘写保护后，对盘中的文件不可以做的是（     ）。", Option = new List<string>()
					{
						"复制文件", "修改文件", "打开文件", "删除文件"
					}, Answer	= new List<int>()
					{
						2, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列设备中，属于输出设备的有（     ）。", Option = new List<string>()
					{
						"键盘", "打印机", "鼠标", "显示器"
					}, Answer	= new List<int>()
					{
						2, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下面那些公司属于国内的公司？", Option = new List<string>()
					{
						"IBM", "TCL", "CISCO", "方正"
					}, Answer	= new List<int>()
					{
						2, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "计算机程序结构包括(        )。", Option = new List<string>()
					{
						"模块结构", "循环结构", "分支", "顺序"
					}, Answer	= new List<int>()
					{
						2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列关于E-MAIL地址的名称中不正确的是(        )。", Option = new List<string>()
					{
						"em@12.com", "ff.cn@66a", "hhff.hh", "cnk#12"
					}, Answer	= new List<int>()
					{
						2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "要将有数据且带有格式的单元格恢复为普通空单元格，先要选定该单元，然后使用（  ）。", Option = new List<string>()
					{
						"\"快捷\"菜单的\"删除\"命令", "Delete键", "\"编辑\"菜单的\"清除\"命令", "工具栏的\"剪切\"工具按钮"
					}, Answer	= new List<int>()
					{
						2, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列设备中，属于输入输出设备的有（    ）。", Option = new List<string>()
					{
						"键盘", "软盘", "硬盘", "显示器"
					}, Answer	= new List<int>()
					{
						2, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "电子邮件的两大特点是(        )和(        )。", Option = new List<string>()
					{
						"不知道收件人的地址也能发送电子邮件", "通过邮件主题，收件人即使不看邮件内容也能大致了解这封邮件", "电子邮件除了发送给单个收件人外，还可以将同一内容抄送给其他人", "电子邮件除了发送给单个收件人外，不可以将同一内容抄送给其他人"
					}, Answer	= new List<int>()
					{
						2, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在Word中如何修改字体的大小？", Option = new List<string>()
					{
						"无法修改", "通过菜单中的字体菜单设置", "通过快捷工具栏中的字体大小设置", "无法设置"
					}, Answer	= new List<int>()
					{
						2, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在PowerPoint中如何设定动画？", Option = new List<string>()
					{
						"用鼠标绘制动画路径", "通过工具栏中的自定义动画设定", "不支持", "右击鼠标选择自定义动画"
					}, Answer	= new List<int>()
					{
						2
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下面哪些是字处理软件？", Option = new List<string>()
					{
						"Word", "Excel", "FrontPage", "Flash", "WPS", "写字板"
					}, Answer	= new List<int>()
					{
						1, 5, 6
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "中央处理器是由（       ）。", Option = new List<string>()
					{
						"运算器", "存储器", "外存储器", "控制器"
					}, Answer	= new List<int>()
					{
						1, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "计算机网络是（     ）相结合的产物。", Option = new List<string>()
					{
						"计算机技术", "计算机软件", "计算机硬件", "通信技术"
					}, Answer	= new List<int>()
					{
						1, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "如何扫描照片？", Option = new List<string>()
					{
						"用扫描仪", "用照相机", "用打印机", "用摄影头"
					}, Answer	= new List<int>()
					{
						1, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "常见的打印机有(    ).", Option = new List<string>()
					{
						"针式打印机", "彩色打印机", "喷墨打印机", "激光打印机"
					}, Answer	= new List<int>()
					{
						1, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "微软拼音输入法包含（       ）三个编辑窗口。", Option = new List<string>()
					{
						"拼音", "光标跟随", "组字", "候选"
					}, Answer	= new List<int>()
					{
						1, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "计算机硬件的工作方式包括（    ）。", Option = new List<string>()
					{
						"单用户方式", "单任务方式", "网络方式", "多用户方式"
					}, Answer	= new List<int>()
					{
						1, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "要制作网页，可以使用网页制作工具如(        )。", Option = new List<string>()
					{
						"FrontPage", "Excel", "Word", "PowerPoint"
					}, Answer	= new List<int>()
					{
						1, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "不是网站的基本信息单位的是(        )。", Option = new List<string>()
					{
						"主页", "网页", "网站", "文档"
					}, Answer	= new List<int>()
					{
						1, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "汉字输出码又称为汉字（    ）。", Option = new List<string>()
					{
						"字型码", "国标码", "字模", "外码"
					}, Answer	= new List<int>()
					{
						1, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "当一张软盘写保护后，对盘中的文件可以做的是（     ）。", Option = new List<string>()
					{
						"复制文件", "移动文件", "打开文件", "删除文件"
					}, Answer	= new List<int>()
					{
						1, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "计算机应用大致可分为（    ）两大类。", Option = new List<string>()
					{
						"数值处理", "科学计算", "非数值处理", "实时控制"
					}, Answer	= new List<int>()
					{
						1, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列设备中，属于输入设备的有（      ）。", Option = new List<string>()
					{
						"键盘", "打印机", "鼠标", "显示器"
					}, Answer	= new List<int>()
					{
						1, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "一个完整的计算机系统由（    ）组成。", Option = new List<string>()
					{
						"软件系统", "软盘和硬盘", "硬件系统", "ROM和RAM"
					}, Answer	= new List<int>()
					{
						1, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "通过WWW可以查询信息，(        )、(        )。", Option = new List<string>()
					{
						"可以阅读多媒体文件", "只能查询文本文件", "可能超文本链接", "只能顺序阅读"
					}, Answer	= new List<int>()
					{
						1, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "如何在Word中插入图片？", Option = new List<string>()
					{
						"粘贴", "直接绘制", "通过插入菜单中的图片插入", "无法绘制"
					}, Answer	= new List<int>()
					{
						1, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "目前在汉字输入方式中，除了常用的键盘输入方式外，还有（     ）。", Option = new List<string>()
					{
						"语音输入方式", "手写输入方式", "计算机自动输入方式", "扫描识别方式"
					}, Answer	= new List<int>()
					{
						1, 2, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "系统软件包括（         ）。", Option = new List<string>()
					{
						"操作系统", "语言处理程序", "数据库管理系统", "常用服务程序"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "计算机病毒的特点是（      ）。", Option = new List<string>()
					{
						"衍生性和潜伏性", "隐蔽性", "破坏性", "传播性"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "目前计算机正向着（    ）方向发展。", Option = new List<string>()
					{
						"巨型化", "网络化", "微型化", "智能化"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列设备中，属于输出设备的是（    ）。", Option = new List<string>()
					{
						"音箱", "耳机", "绘图仪", "打印机"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列设备中，属于输入设备的是（    ）。", Option = new List<string>()
					{
						"扫描仪", "麦克风", "数字化仪", "手写笔"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列等式正确的是（    ）。", Option = new List<string>()
					{
						"1字节＝8个二进制位", "1KB=1024B", "1MB=1024KB", "1GB=1024MB"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "计算机硬件系统由（     ）组成。", Option = new List<string>()
					{
						"运算器", "控制器", "存储器", "输入和输出设备"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "计算机中的指令和数据均以二进制形式表示，其优点是（      ）。", Option = new List<string>()
					{
						"电路简单", "工作可靠、稳定", "运算简单", "逻辑性强"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "计算机中的数据形式有（     ）。", Option = new List<string>()
					{
						"数值", "文字", "图像", "图形"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "以下是声音文件的是(        )。", Option = new List<string>()
					{
						"WAV", "MID", "MP3", "AVI"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "能够相互交流的工具有(        )。", Option = new List<string>()
					{
						"QQ", "UC", "联众", "BBS"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "那些属于杀毒软件？", Option = new List<string>()
					{
						"瑞星", "Kill", "金山毒霸", "KV3000"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "中国自己能够生产那些计算机配件？", Option = new List<string>()
					{
						"CPU", "主板", "内存", "硬盘"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "汉字编码系统是由汉字的（      ）和交换码等组成。", Option = new List<string>()
					{
						"输入码", "内码", "字型码", "ASCII码"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "资源共享包括（    ）。", Option = new List<string>()
					{
						"硬件资源", "软件资源", "数据资源", "工程技术人员"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "按ASCII码值比较大小，下面正确的是（       ）。", Option = new List<string>()
					{
						"数字比字母小", "0比9小", "大写字母比小写字母小", "大小写字母相等"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "人类赖以生存和利用的三项战略是(        )。", Option = new List<string>()
					{
						"物质", "信息", "能量", "信息技术"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列设备中是计算机外存储器的是（    ）。", Option = new List<string>()
					{
						"软盘", "硬盘", "RAM", "文件"
					}, Answer	= new List<int>()
					{
						1, 2
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在Word中如何绘制表格？", Option = new List<string>()
					{
						"通过快捷工具栏中的表格图标托拽绘制", "通过菜单中的表格设定", "用鼠标直接绘制", "用键盘绘制"
					}, Answer	= new List<int>()
					{
						1, 2
					}, User		= user, Catalog = catalog[0], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "对话框与窗口的不同之处是（     ）。", Option = new List<string>()
					{
						"不可以移动位置", "没有标题栏", "不可以最小化", "不可以改变大小"
					}, Answer	= new List<int>()
					{
						3, 4
					}, User		= user, Catalog = catalog[1], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列符号中是文件通配符的是(    ).", Option = new List<string>()
					{
						"@", "&", "*", "?"
					}, Answer	= new List<int>()
					{
						3, 4
					}, User		= user, Catalog = catalog[1], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在Windows系统中，以下说法正确的是（     ）。", Option = new List<string>()
					{
						"只能有一个活动窗口", "只能打开一个窗口", "可同时显示多个窗口", "可同时打开多个窗口"
					}, Answer	= new List<int>()
					{
						1, 3, 4
					}, User		= user, Catalog = catalog[1], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "一个Windows窗口可以被（        ）。", Option = new List<string>()
					{
						"移动", "最大化", "最小化", "改变大小"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[1], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "通常情况下，一个文件的内容可以是（    ）。", Option = new List<string>()
					{
						"学生成绩单", "一个查询程序", "一张图片", "一篇文章"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[1], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "对话框与窗口的共同之处是（     ）。", Option = new List<string>()
					{
						"可以移动位置", "有标题栏", "有关闭按钮", "可以改变大小"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[1], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在资源管理器中，如果误操作将C盘文件删除，可以（    ）。", Option = new List<string>()
					{
						"在回收站对此文件执行\"还原\"操作", "从回收站将此文件拖回原位置", "立即执行\"撤销\"命令", "以上均错"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[1], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "回收站中可以是以下内容（       ）。", Option = new List<string>()
					{
						"文件", "文件夹", "快捷方式", "以上均错"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[1], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "可以为文件设置的属性有（       ）。", Option = new List<string>()
					{
						"只读", "隐藏", "系统", "删除"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[1], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列说法正确的是（    ）。", Option = new List<string>()
					{
						"在回收站对此文件执行\"还原\"操作", "从回收站将此文件拖回原位置", "立即执行\"撤销\"命令", "以上均错"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[1], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在Word中如何修改字体的大小？", Option = new List<string>()
					{
						"无法修改", "通过菜单中的字体菜单设置", "通过快捷工具栏中的字体大小设置", "无法设置"
					}, Answer	= new List<int>()
					{
						2, 3
					}, User		= user, Catalog = catalog[2], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下面哪些是字处理软件？", Option = new List<string>()
					{
						"Word", "Excel", "FrontPage", "Flash", "WPS", "写字板"
					}, Answer	= new List<int>()
					{
						1, 5, 6
					}, User		= user, Catalog = catalog[2], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列操作中能关闭Word应用程序的操作是（      ）。", Option = new List<string>()
					{
						"双击标题栏左边的\"W\"", "单击\"文件\"菜单中的\"关闭\"", "单击标题栏右边的\"关闭\"按钮", "单击\"文件\"菜单中的\"退出\""
					}, Answer	= new List<int>()
					{
						1, 3, 4
					}, User		= user, Catalog = catalog[2], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "利用（     ）可以关闭Word97。", Option = new List<string>()
					{
						"单击标题栏右侧的\"关闭\"按钮", "单击\"文件\"菜单，在下拉菜单中单击\"关闭\"命令", "双击标题栏左侧的控制菜单项", "单击标题栏左侧的控制菜单项，在对应的下拉菜单中单击\"关闭\"命令"
					}, Answer	= new List<int>()
					{
						1, 3, 4
					}, User		= user, Catalog = catalog[2], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "用Word编辑文档时，要将选定的一个文本块复制到该文档的其它地方，可以使用（     ）。", Option = new List<string>()
					{
						"选择\"编辑\"菜单的\"复制\"命令和\"粘贴\"命令", "选择\"编辑\"菜单的\"剪切\"命令和\"粘贴\"命令", "单击工具栏的\"复制\"和\"粘贴\"按钮", "单击工具栏的\"剪切\"和\"粘贴\"按钮"
					}, Answer	= new List<int>()
					{
						1, 3
					}, User		= user, Catalog = catalog[2], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "如何在Word中插入图片？", Option = new List<string>()
					{
						"粘贴", "直接绘制", "通过插入菜单中的图片插入", "无法绘制"
					}, Answer	= new List<int>()
					{
						1, 3
					}, User		= user, Catalog = catalog[2], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在Word编辑状态下，可以通过（    ）操作将已选中的文本块删除。", Option = new List<string>()
					{
						"按下键盘上的Delete键", "按下键盘上的Backspace键", "按下键盘上的Esc键", "单击\"编辑\"菜单中的\"清除\"命令"
					}, Answer	= new List<int>()
					{
						1, 2, 4
					}, User		= user, Catalog = catalog[2], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "能启动Word的方法是（   ）。", Option = new List<string>()
					{
						"从桌面启动", "从\"开始\"菜单启动", "从输入法中启动", "从资源管理器启动"
					}, Answer	= new List<int>()
					{
						1, 2, 4
					}, User		= user, Catalog = catalog[2], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在Word97中，能打印输出当前编辑的文档的操作是（     ）。", Option = new List<string>()
					{
						"单击\"文件\"菜单下的\"打印\"选项", "单击常用工具栏中的\"打印\"按钮", "单击\"文件\"菜单下的\"页面设置\"选项", "单击\"文件\"菜单下的\"打印预览\"选项，再单击工具栏中的\"打印\"按钮"
					}, Answer	= new List<int>()
					{
						1, 2, 4
					}, User		= user, Catalog = catalog[2], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "Word97提供了（       ）文本对齐方式。", Option = new List<string>()
					{
						"两端对齐", "右对齐", "分散对齐", "居中"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[2], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列（     ）是Word97的视图。", Option = new List<string>()
					{
						"普通", "主控文档", "大纲", "联机版式"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[2], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在Word中如何绘制表格？", Option = new List<string>()
					{
						"通过快捷工具栏中的表格图标托拽绘制", "通过菜单中的表格设定", "用鼠标直接绘制", "用键盘绘制"
					}, Answer	= new List<int>()
					{
						1, 2
					}, User		= user, Catalog = catalog[2], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在当前单元格中引用C5单元格地址，混合地址引用是（    ）。", Option = new List<string>()
					{
						"C5", "$C5", "$C$5", "C$5"
					}, Answer	= new List<int>()
					{
						2, 4
					}, User		= user, Catalog = catalog[3], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在Excel中，修改工作表名字的操作可以从（    ）工作表标签开始。", Option = new List<string>()
					{
						"用鼠标左键单击", "用鼠标右键单击", "用鼠标左键双击", "按住Ctrl键的同时用鼠标左键单击"
					}, Answer	= new List<int>()
					{
						2, 3
					}, User		= user, Catalog = catalog[3], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在Excel中，通过（    ）可以修改单元内容。", Option = new List<string>()
					{
						"选中该单元，重新输入新内容", "选中该单元，对编辑栏中出现的原内容进行编辑修改", "单击该单元，并直接在单元格中进行内容的修改", "双击该单元，并直接在单元格中进行内容的修改"
					}, Answer	= new List<int>()
					{
						1, 2, 4
					}, User		= user, Catalog = catalog[3], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "Excel97具有（    ）功能。", Option = new List<string>()
					{
						"电子表格处理", "图形处理", "数据库处理", "文字处理"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[3], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "某区域由A1,A2,A3,B1,B2,B3六个单元格组成，下列能表示该区域的是（    ）。", Option = new List<string>()
					{
						"A1:B3", "A3:B1", "B3:A1", "A1:B1"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[3], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在Excel中，通过（    ）可以将整个工作表全部选中。", Option = new List<string>()
					{
						"单击全选框", "Ctrl+A", "\"编辑\"菜单中的\"全选\"命令", "\"视图\"菜单中的\"全选\"命令"
					}, Answer	= new List<int>()
					{
						1, 2
					}, User		= user, Catalog = catalog[3], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "在PowerPoint中如何设定动画？", Option = new List<string>()
					{
						"用鼠标绘制动画路径", "通过工具栏中的自定义动画设定", "不支持", "右击鼠标选择自定义动画"
					}, Answer	= new List<int>()
					{
						2
					}, User		= user, Catalog = catalog[4], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "要制作网页，可以使用网页制作工具如(        )。", Option = new List<string>()
					{
						"FrontPage", "Excel", "Word", "PowerPoint"
					}, Answer	= new List<int>()
					{
						1, 3, 4
					}, User		= user, Catalog = catalog[5], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列邮件地址合法的是（     ）。", Option = new List<string>()
					{
						"123", "abc", "123@sohu.com", "abc@sohu.com"
					}, Answer	= new List<int>()
					{
						3, 4
					}, User		= user, Catalog = catalog[6], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列关于E-MAIL地址的名称中不正确的是(        )。", Option = new List<string>()
					{
						"em@12.com", "ff.cn@66a", "hhff.hh", "cnk#12"
					}, Answer	= new List<int>()
					{
						2, 3, 4
					}, User		= user, Catalog = catalog[6], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "电子邮件的两大特点是(        )和(        )。", Option = new List<string>()
					{
						"不知道收件人的地址也能发送电子邮件", "通过邮件主题，收件人即使不看邮件内容也能大致了解这封邮件", "电子邮件除了发送给单个收件人外，还可以将同一内容抄送给其他人", "电子邮件除了发送给单个收件人外，不可以将同一内容抄送给其他人"
					}, Answer	= new List<int>()
					{
						2, 3
					}, User		= user, Catalog = catalog[6], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "每个电子邮件信箱都有一个电子邮件地址，下列E-mail地址的格式正确的是（    ）。", Option = new List<string>()
					{
						"djh123@126.com", "xm.com.cn", "123.gov.cn", "lzc369@sohu.com"
					}, Answer	= new List<int>()
					{
						1, 4
					}, User		= user, Catalog = catalog[6], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "计算机网络的主要功能是（     ）。", Option = new List<string>()
					{
						"数据通讯", "会计电算化", "省钱", "资源共享"
					}, Answer	= new List<int>()
					{
						1, 4
					}, User		= user, Catalog = catalog[6], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "不是网站的基本信息单位的是(        )。", Option = new List<string>()
					{
						"主页", "网页", "网站", "文档"
					}, Answer	= new List<int>()
					{
						1, 3, 4
					}, User		= user, Catalog = catalog[6], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "通过WWW可以查询信息，(        )、(        )。", Option = new List<string>()
					{
						"可以阅读多媒体文件", "只能查询文本文件", "可能超文本链接", "只能顺序阅读"
					}, Answer	= new List<int>()
					{
						1, 3
					}, User		= user, Catalog = catalog[6], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "能够相互交流的工具有(        )。", Option = new List<string>()
					{
						"QQ", "UC", "联众", "BBS"
					}, Answer	= new List<int>()
					{
						1, 2, 3, 4
					}, User		= user, Catalog = catalog[6], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "计算机局域网络结构的特点是（      ）。", Option = new List<string>()
					{
						"系统的硬件和数据资源可以共享", "实现分布式处理", "系统的功能和灵活性增强，且更加安全可靠", "当服务器发生故障后整个系统陷于瘫痪"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[6], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "计算机网络分为（     ）。", Option = new List<string>()
					{
						"局域网", "广域网", "国际互联网", "校园网"
					}, Answer	= new List<int>()
					{
						1, 2, 3
					}, User		= user, Catalog = catalog[6], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "下列是顶级域名的是（    ）。", Option = new List<string>()
					{
						"CN", "TW", "COM", "GOV"
					}, Answer	= new List<int>()
					{
						1, 2
					}, User		= user, Catalog = catalog[6], Points = 5
				},
				new AGPQuestionModel_MultiSelect()
				{
					Caption		= "计算机程序结构包括(        )。", Option = new List<string>()
					{
						"模块结构", "循环结构", "分支", "顺序"
					}, Answer	= new List<int>()
					{
						2, 3, 4
					}, User		= user, Catalog = catalog[7], Points = 5
				},
				new AGPQuestionModel_Check()
				{
					Caption = "信息的获取手段可通过感觉器官、科学设备和互联网获得。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "计算机的热启动方式通常是按Ctrl+Alt+Del组合键。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "计算机软件包括系统软件和应用软件两种。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "要在文本中输入“@”字符，采用的方法是Cltr+2（主键盘区）。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "计算机处理速度只跟CPU有关。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "计算机越来越小。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "计算机内的数是以二进制表示的，通常取16个二进制作为一个单元（字节）。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "存储器的容量和性能在计算机的运行能力方面不是一个关键性的成分。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "使用完计算机需要关闭计算机时，可以直接关闭计算机电源。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows98操作系统是应用软件。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "CPU和RAM是计算机的外部设备。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "存储器分为软盘和硬盘两种。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "计算机硬件系统由中央处理器、硬盘、显示器、键盘和鼠标五大基本部件构成。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "一个完整的计算机系统由系统软件和应用软件两部分组成。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "计算机只要有显卡，显示器就能正常工作。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "一般讲，计算机的输出设备有打印机和键盘。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "用拼音输入法输入汉字时，汉字的编码必须用大写字母输入。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "一个汉字系统只需具备汉字输入和汉字输出功能即可。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "汉字内码是为了将汉字输入计算机而编制的代码。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "操作系统的主要功能是管理计算机资源。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "操作系统是软件系统的核心。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "常用的外存储器有软盘、硬盘、光盘、磁带、U盘和移动硬盘等。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "计算机病毒具有以下特点：传播性、破坏性、隐蔽性、潜伏性和衍生性。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "只读存储器（ROM)能永久存放数据和程序，计算机断电后，只读存储器（ROM）中的程序和数据不会丢失。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "传播是指病毒从一个程序或数据文件侵入另一个程序或数据文件的过程。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "计算机中存放所要运行的程序和数据，供微处理器分析处理的部分是内存储器。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "鼠标是计算机最常用的输入设备。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "应用软件通常面向最终用户并具有特定的功能。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "通常把计算机软件分为两大类：系统软件和应用软件。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "外存储器既是计算机的输入设备，又是计算机的输出设备。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "一台微机必备的输入输出设备是：键盘、鼠标、显示器。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "外存储器是计算机的外部设备。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "外存储器又称为辅助存储器，用来永久地存放大量的程序和数据。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "存储器的主要功能是保存信息。存储器分为两大类型：内存储器和外存储器。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "运算器和控制器构成了中央处理器CPU。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "指令是指挥计算机硬件工作的命令，一组有序的指令集合就构成了程序。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "只有硬件没有软件的计算机通常称为“裸机”。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "CPU的主频是指CPU工作时的时钟频率。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在ASCII码表中，数字的ASCII码值小于字母的ASCII值。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "《信息交换用汉字编码字符集基本集》中的汉字根据其使用频率和用途分为二级。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "汉语拼音汉字输入技术属音码输入技术。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "信息交换汉字编码字符集（基本集）GB2312-80简称为国标码。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "一个完整的计算机系统由计算机硬件系统和计算机软件系统组成。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "计算机中的“数据”是一个广义的概念，包括数值、文字、图形、图像、声音等多种形式。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "计算机内部采用二进制表示数据，其优点是：电路简单、工作可靠并稳定、运算简单、逻辑性强。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在计算机中采用二进制的形式表示数据和指令。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "对话框可以被改变大小、最大化、最小化。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "用户不能在WINDOWS的桌面上创建新文件夹。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows98的任务栏可以放在桌面的任何位置。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows98具有多媒体功能，在不加其它专用软件的情况下，可以播放音乐， 也可以收看电视节目。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows 98中，被删除的文件或文件夹将存放在TEMP文件夹中。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows 98界面中的任务栏总是在最前面，不能自动隐藏。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows 98系统中，打印机无需设置属性就可自动打印横向或纵向的格式。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows 98允许同时有多个活动窗口存在。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows 98，查找文件是不会区分大小写的。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows 98应用程序只能从\"开始\"菜单启动。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows 98中，其任务栏只能位于屏幕的最下面。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows 98中，当文件或文件夹被删除并放入回收站后，它就就再占用磁盘空间。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows 98中对回收站的操作就能删除回收站的部分文件。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WIDOWS98中，鼠标的双击速度是不能改变的。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WIDOWS98的记事本中操作中，复制文本内容和移动文本内容的操作是一样的。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "\"回收站\"中的文件是被删除的文件，它是不占磁盘空间的。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WIDOWS98和DOS系统使用相同的文件名命名规则。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "利用WIDOWS98的\"资源管理器\"中的删除命令删除了文件或文件夹，则该文件夹将将彻底删除。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WINDOWS中不用物理键盘就不可能向编辑的文件中输入字符。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "中文WIDOWS98本身就带有智能ABC、全拼、五笔字型等输入法。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WIDOWS98中，用画笔程序作图，对图中利用文本工具已输入完毕的文字可在图形编辑的任何时候进行编辑和重排。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WIDOWS98显示设置中\"屏幕区域\"的大小范围，由背景图片的大小决定。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WIDOWS98的水平滚动条又名滑动条。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "当WIDOWS98窗口最大化后中，点击\"还原\"按钮则窗口被最小化。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WIDOWS98的卡片式对话框可以被改变大小，而信息显示对话框不能被改变大小。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WIDOWS桌面上的\"我的文档\"中只能存放非应用程序。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "如果计算机配置了光驱，就可以用WIDOWS98的\"CD播放器\"播放CD唱片和MP3碟片。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WIDOWS98的窗口最小化，实际上意味着相对应的程序暂时被关闭。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WIDOWS98中更改的计算机日期：只能将日期改为过去的日期，而不能改为将来的日期。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WIDOWS资源管理器窗口中，单击某一文件夹的图标就能看到该文件夹的所有内容。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "一台计算机只要安装了WINDOWS98操作系统术可以拨号上网了。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WINDOWS98系统具有对文件重命名功能，它可以对任意文件进行重命名而不影响系统进行。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "只有在WINDOWS98系统中安装了打印机，任何打印机连接上后就可正确打印。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "利用WINDOWS98的画图程序可以处理各种格式的图形文件。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WINDOWS98中，要创建一个名字为ABC。DOC的文档，可利用\"文件\"菜单中的\"打开\"命令，在\"打开\"文件对话框中输入文件名，就可以实现。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WINDOWS98中不能彻底删除文件，只能将删除的文件放在加收站中。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WINDOWS98中的标准形计算器和科学计算器的功能是完全一样的。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "所有的Windows98窗口中一定有滚动条存在。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows的资源管理器只能管理文件和文件夹。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "利用Windows98的记事本可以编辑各种格式的文件。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "DOS窗口下，不能用DOS的命令删除Windows98的文件。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows98中，可启动标准型计算器来执行函数运算和完成统计计算。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows中，能自动识别打印纸张的大小。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows不允许用户进行系统（config）设置。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "启动Windows后，屏幕上看到的大片区域叫窗口。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "用户通过Windows98所提供的对话框，向系统提供执行命令所需的参数，如打开文件的类型、文件所在的位置等。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "当打开多个窗口时，当前活动窗口的标题栏为蓝色，其他窗口标题栏是灰色。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在使用完计算机需要关闭计算机时，必须用关机命令正确地退出Windows98，不应该在Windows98仍然运行的情况下关闭计算机电源。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows98是按树型结构组织文件夹和文件的。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在一组复选菜单命令中可以同时选择多个命令。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "通过拖动窗口的标题兰，可以改变该窗口在屏幕上的位置。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows98把用户删除的硬盘上的文件、文件夹和快捷方式放入回收站。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "双击“我的电脑”图标，可以打开“我的电脑”窗口；通过“我的电脑”，用户可以浏览、管理计算机资源。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "非法退出Windows98可能会导致数据丢失，则在下次启动计算机时，Windows98将作磁盘检测。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows98正确启动后，呈现在用户面前的整个屏幕区域称为桌面。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows98中，在同一盘之间拖动文件或文件夹的同时按下Ctrl键，可以复制文件或文件夹。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows98提供了一套完整的磁盘管理功能（如工具Scan Disk Space Agency等）能保持系统无错运行在最佳状态。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows98支持长文件名，最多可达255个字符，且可包含有空格。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows98的\"我的电脑\"中，不仅可以进行文件管理，还可以进行磁盘管理。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows98中，桌面指的是窗口、图标和对话框所在的屏幕背境。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows 98系统可以和MS-DOS系统切换使用。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows 98操作系统下，当使用完计算机后，通过关断电源来退出系统可能导致文件损坏或破坏Windows 98系统的就良后果。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows 98的窗口是可以移动位置的。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows 98允许一个用户使用多个程序。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows 98中，媒体播放器既可以音频，也可以视频。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows 98中，利用控制面板窗口中的\"日期/时间\"图标，可改变日期。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows 98系统下，把文件放入回收站并不意味文件一定从磁盘上清除了。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows 98是美国微软公司（Microsoft）推出的图形化操作系统。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WINDOWS的文件目录结构是树型结构。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows的资源管理器窗口中，要想显示隐含文件，可以利用\"查看\"设置。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows 98中可以执行DOS命令。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WIDOWS98中，要关闭MS-DOS提示符下键入EXIT命令并回车，或者单击DOS窗口右上角的\"关闭\"按钮。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WIDOWS98系统和MS-DOS系统可以相互切换使用。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "画图程序是WIDOWS98系统中的附件程序之一。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "可以用WIDOWS98的\"计算器\"程序直接进行十六进制数的加减。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WIDOWS资源管理器中可以看到某个目录下的全部文件字节数之和。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WIDOWS98系统下，可以按文件的创建时间来查找文件。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "可以用WIDOWS98的\"媒体播放器\"来播放VCD影碟。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WIDOWS98操作系统中，可以用键盘来执行菜单命令。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "用户可以定制WIDOWS98关闭时的声音。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WIDOWS98系统可以同时运行多个应用程序。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "运用WIDOWS98时，从标准型计算器可以转入科学型计算器。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WIDOWS98系统中，通过\"我的电脑\"可以查看\"我的文档\"中的文件。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WINDOWS98操作中，弹出快捷菜单一般单击鼠标右键。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "WINDOWS98中的应用程序可以从\"开始\"菜单中启动或者从桌面直接启动。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WINDOWS98中，\"资源管理器\"和\"我的电脑\"对文件夹管理其效果是类似的。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WINDOWS98中对软盘进行格式化，则会将软盘上的数据全部清除，而且不可恢复。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows98系统中，共享的资源可以方便的设置为不共享。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows98可采用FAT 16或FAT 32文件系统，而DOS只能采用FAT16文件系统。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "可以在Windows98的\"画图\"程序中输入各种颜色的汉字。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows98的鼠标双击速度可以通过相应的程序调整。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "利用Windows98的MS-DOS方式模拟的DOS环境可以运行一些需要在DOS状态下运行的程序。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Windows98的活动窗口的标题栏的颜色可由用户自行设定。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "用鼠标把Windows98桌面上的图标拖放到任务栏的\"快速启动区\"的结  果是在\"快速启动区\"再建一个快捷方式。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows中，对菜单的选择，可以直接用鼠标单击菜单栏中相应的菜单项，也可以按该菜单对应的组合键。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Windows98提供的记事本中，设置的字体对整个文本文件都有效，因而不能分段设置不同的字体。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在WORD97中对文档的页面设置后，不能再进行修改。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[2], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "通过执行Word“文件”菜单中的关闭命令，可以退出Word应用程序。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[2], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Word中，通过键盘输入文档内容时，当输入至一行末尾时，系统可以自动换行。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[2], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "Word97可以做到录入与排版同步进行，随时在屏幕上显示出文件最终的输出样式，真正做到了所见即所得。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[2], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Word编辑状态下，可以通过双击状态栏上的“改写”按钮将系统从插入状态转换为改写状态。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[2], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Word中，为选定文本定义字号时，阿拉伯字号值（如12、16、20等）越大，实际显示的字将越大。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[2], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Excel中，输入的字符型数据在单元格中自动右对齐。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[3], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Excel中，使用Ctrl+Home键可以使活动单元格快速移动到当前行的最左侧单元。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[3], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Excel中，当按下End键之后，当前单元并无任何变化，仅是屏幕底部的状态栏中出现了状态提示符END，此时再按上、下、左、右移动光标键，则活动单元格移到光标移动键所指方向的下一个内容区域的第一个单元，并且状态行的提示信息END自动消失。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[3], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Excel中，单击选中区域之外的任何一个单元，就可以释放选中的区域。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[3], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "在Excel中，单击某单元，然后将鼠标指针移向另一单元，按下Shift键的同时单击左键，将选中一个连续的区域。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[3], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "IP地址不是唯一的，可以重复。", Answer = false, User = user, Verifier = verifier, Catalog = catalog[6], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "资源共享包括硬件资源、软件资源和数据资源的共享。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[6], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "每一个电子邮件信箱都有一个电子邮件地址。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[6], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "计算机网络是计算机技术和通讯技术相结合的产物。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[6], Points = 2
				},
				new AGPQuestionModel_Check()
				{
					Caption = "域名是分层次的，每层之间用“.”分隔，最右侧是最高层域名，或称顶级域名。", Answer = true, User = user, Verifier = verifier, Catalog = catalog[6], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "在计算机中通常以___为单位传送信息。", Answer = "字", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "标准的Windows窗口由标题栏、菜单栏、工具栏和___等组成。", Answer = "状态栏", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "软件是程序指令的集合，软件系统可分为___、___和支持软件。", Answer = "系统软件,应用软件", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "在计算机中信息储存的最小单位是___。", Answer = "位", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "拼音输入码属于汉字编码中的___。", Answer = "外码", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "结构化程序设计是指程序设计中主要采用___、___和___基本结构编写。", Answer = "顺序,选择,循环", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "五笔字型基本字根按___分为五大区。", Answer = "起笔的笔画", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "信息技术是指___的技术，其中___技术是信息技术的核心。", Answer = "获取、应用、存储和传输信息,计算机技术", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "五笔字型中，“一”提视为___，“丶”视为___。", Answer = "横,捺", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "在21世纪的今天，信息与物质、能源构成人类生存和社会发展不可缺少的三在要素，信息正向着___、___、___、___和多媒体方向飞速发展。", Answer = "高速化,数字化,网络化,智能化", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "计算机中的所有数据都是由___进制数码表示。", Answer = "二", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "从构成计算机的___不同，计算机可分为___代。", Answer = "电子元器件,四", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "计算机硬件主要由___、___、___、___和___五大部分所组成。", Answer = "存储器,运算器,控制器,输入设备,输出设备", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "计算机程序必须位于___内，计算机才能执行其中的指令。", Answer = "存储器", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "Word是文字处理软件的一种，新建一个Word文档，它的默认文件扩展名为___。", Answer = "DOC", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "用光电技术制成的CD-ROM一张能存储___MB信息；DVD一张能存储___GB信息。", Answer = "650,3.4", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "存储器的1MB相当于___KB。", Answer = "", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "字母“A”的ASCII码是十进制数65，字母“B”的ASCII是十进制数___。", Answer = "", User = user, Verifier = verifier, Catalog = catalog[0], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "标准的Windows窗口由标题栏、菜单栏、工具栏和___等组成。", Answer = "状态栏", User = user, Verifier = verifier, Catalog = catalog[1], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "Word97默认的字是___号。", Answer = "五", User = user, Verifier = verifier, Catalog = catalog[2], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "Word97默认的字体是___。", Answer = "宋体", User = user, Verifier = verifier, Catalog = catalog[2], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "Word是文字处理软件的一种，新建一个Word文档，它的默认文件扩展名为___。", Answer = "DOC", User = user, Verifier = verifier, Catalog = catalog[2], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "在Excel单元格中，若未设置特定格式，则文本数据会___对齐。", Answer = "左", User = user, Verifier = verifier, Catalog = catalog[3], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "在Excel单元格中，若未设置特定格式，则数值数据会___对齐。", Answer = "右", User = user, Verifier = verifier, Catalog = catalog[3], Points = 2
				},
				new AGPQuestionModel_Blank()
				{
					Caption = "工资计算公式定义中各项栏目的类型，凡是参与计算的都应是___。", Answer = "数值型", User = user, Verifier = verifier, Catalog = catalog[3], Points = 2
				},
				new AGPQuestionModel_ShortAnswer()
				{
					Caption = "简述网络的定义和功能？", Answer = "略", User = user, Verifier = verifier, Catalog = catalog[0], Points = 10
				},
				new AGPQuestionModel_ShortAnswer()
				{
					Caption = "什么是搜索引擎？如何使用它在网上搜索需要的信息？怎样将网页保存在自己的计算机中？", Answer = "略", User = user, Verifier = verifier, Catalog = catalog[0], Points = 10
				},
				new AGPQuestionModel_ShortAnswer()
				{
					Caption = "简述电子邮件有什么特点？常用的电子邮件软件有哪些？", Answer = "略", User = user, Verifier = verifier, Catalog = catalog[0], Points = 10
				},
				new AGPQuestionModel_ShortAnswer()
				{
					Caption = "用QBasic编程求底为20、高为3的三角形的面积。", Answer = "略", User = user, Verifier = verifier, Catalog = catalog[0], Points = 10
				},
				new AGPQuestionModel_ShortAnswer()
				{
					Caption = "简述计算机的工作原理。", Answer = "略", User = user, Verifier = verifier, Catalog = catalog[0], Points = 10
				},
				new AGPQuestionModel_ShortAnswer()
				{
					Caption = "简述网络的定义和功能？", Answer = "略", User = user, Verifier = verifier, Catalog = catalog[6], Points = 10
				},
				new AGPQuestionModel_ShortAnswer()
				{
					Caption = "什么是搜索引擎？如何使用它在网上搜索需要的信息？怎样将网页保存在自己的计算机中？", Answer = "略", User = user, Verifier = verifier, Catalog = catalog[6], Points = 10
				},
				new AGPQuestionModel_ShortAnswer()
				{
					Caption = "简述电子邮件有什么特点？常用的电子邮件软件有哪些？", Answer = "略", User = user, Verifier = verifier, Catalog = catalog[6], Points = 10
				},
				new AGPQuestionModel_ShortAnswer()
				{
					Caption = "用QBasic编程求底为20、高为3的三角形的面积。", Answer = "略", User = user, Verifier = verifier, Catalog = catalog[7], Points = 10
				},
			};

			{
				var question_list = new List<Question>();
				var rand = new Random();
				foreach (var q in questions)
				{
					var qq = q.ConvertToQuestion();
					question_list.Add(qq);
					question_list.Add(GenQuestion(rand, qq));
					question_list.Add(GenQuestion(rand, qq));
					question_list.Add(GenQuestion(rand, qq));
					question_list.Add(GenQuestion(rand, qq));
					question_list.Add(GenQuestion(rand, qq));
					question_list.Add(GenQuestion(rand, qq));
					question_list.Add(GenQuestion(rand, qq));
					question_list.Add(GenQuestion(rand, qq));
					question_list.Add(GenQuestion(rand, qq));
				}
				context.Questions.AddRange(question_list);
			}

			context.SaveChanges();
		}
	}
}
