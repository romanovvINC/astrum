using Telegram.Bot;

namespace FuckWeb.Features.Notifier;

public class TgNotifier
{
    public static async void Notify(string message)
    {
        var bot = new TelegramBotClient("5336780164:AAEdtqMW2uwAnoT5jaj18be1O9fpdxzLOM8");

        await bot.SendTextMessageAsync(-493649718, message);
    }
}