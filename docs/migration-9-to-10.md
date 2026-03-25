# مهاجرت از نسخه 9 به 10

## هدف

هدف این سند ثبت روند مهاجرت Zamin از .NET 9 به .NET 10، تصمیم‌های مهم، و ترتیب اجرای migration است.

## تغییرات زیرساخت build

در این مرحله، زیرساخت build و packaging به‌صورت متمرکز بازبینی و تنظیم شد:

- اضافه شدن `global.json` برای کنترل نسخه SDK
- اضافه شدن `Directory.Build.props` در ریشه repository
- اضافه شدن `Directory.Packages.props` در ریشه repository
- تعریف `Directory.Build.props` محلی برای solution مربوط به abstractionها
- فعال‌سازی تولید package هنگام build برای موج اول

## موج اول

موج اول شامل packageهای abstraction پایدار و پایه framework است:

- `Zamin.Extensions.Caching.Abstractions`
- `Zamin.Extensions.DependencyInjection.Abstractions`
- `Zamin.Extensions.Events.Abstractions`
- `Zamin.Extensions.ChangeDataLog.Abstractions`
- `Zamin.Extensions.ObjectMappers.Abstractions`
- `Zamin.Extensions.Serializers.Abstractions`
- `Zamin.Extensions.UsersManagement.Abstractions`
- `Zamin.Extensions.MessageBus.Abstractions`
- `Zamin.Extensions.Translations.Abstractions`

این packageها به این دلیل در موج اول قرار گرفتند که قراردادهای نسبتاً پایدار و نقش زیرساختی دارند و پایه implementationهای بعدی محسوب می‌شوند.

## خارج از موج اول

package زیر فعلاً از موج اول خارج شده است:

- `Zamin.Extensions.Logger.Abstractions`

### دلیل
مسئولیت این پروژه بیشتر شبیه نگهداری ثابت‌ها و event idهای logging است تا یک abstraction واقعی. بنابراین قبل از migration نیازمند تصمیم معماری درباره ماندن، ادغام یا حذف است.

## نکات مهم

- در موج اول هیچ بازطراحی عمیقی انجام نمی‌شود.
- تغییرات این مرحله صرفاً با هدف migration کنترل‌شده و آماده‌سازی نسخه 10 انجام می‌شود.
- implementation packageها در موج‌های بعدی مهاجرت خواهند کرد.# آشنایی با Zamin

## به‌روزرسانی مدیریت dependencyها

در مسیر migration به .NET 10، برای abstraction packageهای موج اول، مدیریت نسخه dependencyها به‌صورت متمرکز تنظیم شد.

در این مرحله:
- `Directory.Packages.props` برای solution abstractionها اضافه شد
- نسخه dependencyهای مشترک از فایل‌های پروژه حذف و به فایل مرکزی منتقل شد
- dependency مربوط به `Microsoft.Extensions.Logging.Abstractions` نیز برای هماهنگی با موج اول به نسخه 10 منتقل شد
