# برنامه ارتقا و بازطراحی Zamin

## مقدمه

این سند راهبرد کلی مهاجرت Zamin از .NET 9 به .NET 10 را مشخص می‌کند.

هدف این مهاجرت صرفاً ارتقا نسخه فریم‌ورک نیست، بلکه شامل:

- تثبیت زیرساخت build و packaging
- تمیزکاری ساختار پروژه
- آماده‌سازی برای بازطراحی بخش‌های پیچیده
- ایجاد مسیر توسعه پایدار برای آینده

Zamin به عنوان بخشی از اکوسیستم **ARO-Mind** در حال توسعه است و این migration گامی مهم در بلوغ این اکوسیستم محسوب می‌شود.

---

## اصول راهبردی

در این migration چند اصل کلیدی رعایت می‌شود:

### 1. مهاجرت مرحله‌ای (Wave-based)
به جای ارتقا یک‌باره کل پروژه، migration در چند موج انجام می‌شود.

### 2. حداقل تغییر در موج اول
در موج اول تمرکز فقط روی migration است، نه redesign.

### 3. حفظ سازگاری
تا حد ممکن از ایجاد breaking change جلوگیری می‌شود.

### 4. تفکیک abstraction و implementation
ابتدا abstractionها مهاجرت داده می‌شوند، سپس implementationها.

---

## زیرساخت build

در این مرحله، زیرساخت build و packaging به‌صورت متمرکز بازطراحی شده است.

### اجزای اصلی:

- `global.json` → تعیین نسخه SDK
- `Directory.Build.props` (ریشه) → تنظیمات مشترک
- `Directory.Packages.props` → مدیریت نسخه dependencyها
- `Directory.Build.props` محلی → override برای waveها

### نکته مهم

از آنجا که MSBuild پس از یافتن اولین `Directory.Build.props` جستجو را متوقف می‌کند، فایل‌های محلی به‌صورت صریح فایل ریشه را import می‌کنند.

این ساختار امکان:

- مدیریت مرکزی
- override کنترل‌شده
- migration مرحله‌ای

را فراهم می‌کند.

---

## موج اول (Wave 1)

### هدف
مهاجرت abstraction packageهای پایه به .NET 10

### packageهای شامل:

- Zamin.Extensions.Caching.Abstractions
- Zamin.Extensions.DependencyInjection.Abstractions
- Zamin.Extensions.Events.Abstractions
- Zamin.Extensions.ChangeDataLog.Abstractions
- Zamin.Extensions.ObjectMappers.Abstractions
- Zamin.Extensions.Serializers.Abstractions
- Zamin.Extensions.UsersManagement.Abstractions
- Zamin.Extensions.MessageBus.Abstractions
- Zamin.Extensions.Translations.Abstractions

### دلیل انتخاب

این packageها:

- contractهای نسبتاً پایدار دارند
- وابستگی‌های پیچیده ندارند
- پایه implementationهای دیگر هستند

---

## خارج از موج اول

### Zamin.Extensions.Logger.Abstractions

این پروژه فعلاً از migration خارج شده است.

#### دلیل:

- abstraction واقعی محسوب نمی‌شود
- بیشتر نقش نگهداری EventId و ثابت‌های logging را دارد
- نیازمند تصمیم معماری (حذف / ادغام / تغییر ساختار)

---

## موج‌های بعدی

### Wave 2
مهاجرت implementation packageها:

- Caching implementations
- ObjectMappers implementations
- Serializers implementations
- MessageBus implementations
- Events implementations

### Wave 3
تکمیل sampleها و endpointها:

- Zamin.EndPoints
- Sample projects
- Integration tests (در صورت اضافه شدن)

---

## بخش‌های نیازمند بازطراحی

این بخش‌ها در مراحل بعدی نیاز به بازبینی جدی دارند:

### MessageBus
- طراحی فعلی پیچیده و سخت‌فهم است
- نحوه اتصال consumerها نیاز به ساده‌سازی دارد

### Translations
- وابستگی به culture در runtime مشکل‌ساز است
- storage strategy نیاز به بازطراحی دارد

### Auth (ApiAuthentication)
- ساختار فعلی پیچیده و خارج از کنترل کامل است
- نیاز به ساده‌سازی و استانداردسازی دارد

### Logging
- ساختار abstraction مشخص نیست
- نیاز به تصمیم درباره حذف یا redesign

### SoftwarePartDetector
- ایده قوی است ولی نیاز به تثبیت API دارد

---

## ترتیب اجرای کلی

1. تثبیت build system
2. مهاجرت abstractionها (Wave 1)
3. انتشار packageهای پایه
4. مهاجرت implementationها (Wave 2)
5. تکمیل sampleها
6. بازطراحی بخش‌های پیچیده

---

## نتیجه مورد انتظار

پس از اتمام این مراحل:

- ساختار پروژه تمیزتر و قابل فهم‌تر خواهد بود
- build و packaging یکدست می‌شود
- migration به .NET 10 کامل می‌شود
- بستر مناسبی برای توسعه‌های بعدی در ARO-Mind فراهم می‌شود