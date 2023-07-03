using MyDotNetCoreCodeBase.Middleware;

using Serilog;
using Serilog.Sinks.Email;

using System.Net;

// serilog�]�w
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // �Ӽh�ťH�U���O
    .WriteTo.Console() // �NLog��X��׺ݾ�
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // �NLog��X���ɮ�,�R�W�H��Ѥ�����Ϥ�
    // mail�]�w
    .WriteTo.Email(new EmailConnectionInfo()
    {
        MailServer = "smtp.gmail.com",
        Port = 465,
        EnableSsl = true,
        NetworkCredentials = new NetworkCredential("�b��", "�K�X"),
        EmailSubject = "Error Notification",
        FromEmail = "�H�H��",
        ToEmail = "���H��",
    })
    .CreateLogger();
// �ն]log
//try
//{
//    Log.Information("�Ұʶ}�l");
//    int.Parse("aaaa");
//    return 0;
//}
//catch (Exception ex)
//{
//    Log.Fatal(ex, "�o�ͨҥ~");
//    return 1;
//}
//finally
//{
//    Log.CloseAndFlush(); // �{���n�����e�I�s
//}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// �n����ϥ�middleware�[�o�q
//app.UseMiddleware<CustomerExceptionMiddleware>();

// �D�}�o����
if (!app.Environment.IsDevelopment())
{
    // ���~�ɨ�o��
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

// �ۭq�{���X�өw�q Middleware �]�i�H�Q��next���I�s�M�w�O�_�n�i�J�U��Middleware
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

// �׺ݩe�U ��o��᭱�N����
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

// Map �|�̾ګ��w�۲Ū��ШD���|�ШDPipeline�C�p�G�ШD���|�H���w���|�}�l�A�h�������
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


