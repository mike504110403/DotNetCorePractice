using MyDotNetCoreCodeBase.Middleware;

using Serilog;
using Serilog.Sinks.Email;

using System.Net;

// serilog設定
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // 該層級以下都記
    .WriteTo.Console() // 將Log輸出到終端機
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // 將Log輸出為檔案,命名以當天日期為區分
    // mail設定
    .WriteTo.Email(new EmailConnectionInfo()
    {
        MailServer = "smtp.gmail.com",
        Port = 465,
        EnableSsl = true,
        NetworkCredentials = new NetworkCredential("帳號", "密碼"),
        EmailSubject = "Error Notification",
        FromEmail = "寄信者",
        ToEmail = "收信者",
    })
    .CreateLogger();
// 試跑log
//try
//{
//    Log.Information("啟動開始");
//    int.Parse("aaaa");
//    return 0;
//}
//catch (Exception ex)
//{
//    Log.Fatal(ex, "發生例外");
//    return 1;
//}
//finally
//{
//    Log.CloseAndFlush(); // 程式要結束前呼叫
//}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 要全域使用middleware加這段
//app.UseMiddleware<CustomerExceptionMiddleware>();

// 非開發環境
if (!app.Environment.IsDevelopment())
{
    // 錯誤導到這頁
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 自訂程式碼來定義 Middleware 也可以利用next的呼叫決定是否要進入下個Middleware
//if (app.Environment.IsDevelopment())
//{
//    app.Use(async (context, next) => {
//        await context.Response.WriteAsync($"11111{Environment.NewLine}");
//        await next();
//        await context.Response.WriteAsync($"22222{Environment.NewLine}");
//    });
//    app.Run(async context => {
//        await context.Response.WriteAsync($"33333{Environment.NewLine}");
//    });
//}

// 終端委託 到這邊後面就不做
app.Run();
if (app.Environment.IsDevelopment())
{
    app.Run(async context => {
        await context.Response.WriteAsync($"111111{Environment.NewLine}");
    });
    app.Run(async context => {
        await context.Response.WriteAsync($"22222{Environment.NewLine}");
    });
}

// Map 會依據指定相符的請求路徑請求Pipeline。如果請求路徑以給定路徑開始，則執行分支
//app.Map("/111111", One);
//app.Map("/222222", Two);
//app.Run();
//static void One(IApplicationBuilder app)
//{
//    app.Run(async context => {
//        await context.Response.WriteAsync($"You are 111111{Environment.NewLine}");
//    });
//}
//static void Two(IApplicationBuilder app)
//{
//    app.Run(async context => {
//        await context.Response.WriteAsync($"You are 222222{Environment.NewLine}");
//    });
//}


