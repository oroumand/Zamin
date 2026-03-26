# تغییرات

تمام تغییرات مهم این پروژه در این فایل ثبت می‌شود.

---

## [10.0.0] - 2026-03-25

### اضافه شده (Added)

- مهاجرت موج اول پکیج‌های abstraction به .NET 10
- اضافه شدن زیرساخت build مرکزی با استفاده از:
  - Directory.Build.props
  - global.json
- اضافه شدن مدیریت متمرکز نسخه پکیج‌ها (Central Package Management)
- اضافه شدن آیکون مشترک برای همه پکیج‌ها
- اضافه شدن مستندات:
  - راهبرد migration
  - ساختار build و packaging
  - معرفی پروژه و اکوسیستم

### تغییر یافته (Changed)

- ارتقا TargetFramework پکیج‌های موج اول به `net10.0`
- یکدست‌سازی metadata پکیج‌ها (Company، Repository، Icon و ...)
- به‌روزرسانی dependencyها برای سازگاری با .NET 10
- انتقال مدیریت نسخه dependencyها به `Directory.Packages.props`

### نکات (Notes)

- این نسخه شامل اولین موج مهاجرت است و فقط پکیج‌های abstraction پایدار را شامل می‌شود
- هیچ بازطراحی اساسی در این مرحله انجام نشده است
- تمرکز این نسخه بر تثبیت زیرساخت و آماده‌سازی برای مراحل بعدی است

### خارج از این نسخه

پکیج زیر در این نسخه منتشر نشده است:

- `Zamin.Extensions.Logger.Abstractions`

دلیل:
این پروژه هنوز به عنوان یک abstraction واقعی تثبیت نشده و نیازمند تصمیم معماری برای ادامه مسیر (حذف، ادغام یا بازطراحی) است

### مهاجرت های موج دوم
- مهاجرت `Zamin.Extensions.Logger.Abstractions` به `.NET 10` برای تکمیل family مربوط به abstractionها
- ثبت رسمی deprecated بودن این package برای بازبینی در نسخه 11

- مهاجرت `Zamin.Extensions.ObjectMappers.AutoMapper` به `.NET 10`
- مهاجرت `Zamin.Extensions.ObjectMappers.Mapster` به `.NET 10`
- هماهنگ‌سازی dependencyهای خانواده ObjectMappers با abstractionهای نسخه 10
- مهاجرت `Zamin.Extensions.Translations.Parrot` به `.NET 10`
- مهاجرت `Zamin.Extensions.Serializers.EPPlus` به `.NET 10`
- مهاجرت `Zamin.Extensions.Serializers.Microsoft` به `.NET 10`
- مهاجرت `Zamin.Extensions.Serializers.NewtonSoft` به `.NET 10`
- مهاجرت `Zamin.Extensions.Caching.InMemory` به `.NET 10`
- مهاجرت `Zamin.Extensions.Caching.Distributed.Redis` به `.NET 10`
- مهاجرت `Zamin.Extensions.Caching.Distributed.Sql` به `.NET 10`
- مهاجرت `Zamin.Extensions.DependencyInjection` به `.NET 10`
- مهاجرت `Zamin.Extensions.ChangeDataLog.Hamster` به `.NET 10`
- مهاجرت `Zamin.Extensions.ChangeDataLog.Sql` به `.NET 10`
- مهاجرت `Zamin.Extensions.UsersManagement` به `.NET 10`