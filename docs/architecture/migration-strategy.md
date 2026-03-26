<div dir="rtl">

# راهبرد مهاجرت و بازطراحی Zamin

## مقدمه

هدف این سند مشخص کردن مسیر مهاجرت کامل Zamin از `.NET 9` به `.NET 10` و تعیین نحوه برخورد با بهبودهای معماری، تغییرات داخلی و تغییرات breaking است.

در این مرحله، راهبرد پروژه از مهاجرت موجی محدود به یک مهاجرت کامل و کنترل‌شده تغییر کرده است. دلیل این تصمیم این است که نیمه‌مهاجرت ماندن پروژه، هم در توسعه داخلی مشکل ایجاد می‌کند و هم باعث می‌شود برخی packageها، sampleها و dependencyها به‌درستی build نشوند.

نمونه این مسئله در وابستگی برخی packageها به پیاده‌سازی‌های دیگر دیده می‌شود؛ جایی که باقی ماندن بخشی از پروژه روی نسخه 9 مانع build سالم کل زنجیره می‌شود.

## هدف نسخه 10

در نسخه 10، هدف این موارد است:

- مهاجرت کامل کل framework به `.NET 10`
- یکدست‌سازی build و packaging
- تکمیل و تثبیت مستندات
- انجام بهبودهای داخلی و non-breaking
- شناسایی و deprecate کردن بخش‌هایی که در آینده باید حذف یا بازطراحی شوند

## هدف نسخه 11

در نسخه 11، هدف این موارد خواهد بود:

- حذف APIها و packageهای deprecated شده
- اعمال breaking changeهای معماری
- بازطراحی بخش‌هایی که در نسخه 10 فقط علامت‌گذاری شده‌اند

## اصول راهبردی

### اصل اول: مهاجرت کامل قبل از بازطراحی عمیق

ابتدا کل پروژه به `.NET 10` مهاجرت داده می‌شود تا همه packageها، sampleها و dependencyها در یک وضعیت پایدار و یکدست قرار بگیرند.

### اصل دوم: انجام بهبودهای non-breaking در نسخه 10

هر بهبودی که از بیرون پروژه دیده نمی‌شود یا قرارداد عمومی را نمی‌شکند، در همین نسخه انجام می‌شود.

### اصل سوم: defer کردن breaking changeها

هر تغییری که باعث شکستن مصرف‌کننده‌ها شود، در نسخه 10 فقط deprecate می‌شود و حذف واقعی آن به نسخه 11 موکول خواهد شد.

### اصل چهارم: حذف محدود و کم‌هزینه در همین نسخه

اگر در برخی بخش‌ها، به‌ویژه در `Root Utilities`، پروژه یا قابلیتی وجود داشته باشد که مصرف بیرونی مهمی نداشته باشد و حذف آن ripple effect زیادی ایجاد نکند، حذف آن در همین نسخه 10 مجاز است.

## دسته‌بندی تغییرات

### دسته A: مهاجرت مستقیم

این دسته شامل پروژه‌هایی است که فقط باید به `.NET 10` مهاجرت داده شوند و معمولاً نیاز به بازطراحی ندارند.

نمونه‌ها:
- implementationهای ساده‌تر در `ObjectMappers`
- implementationهای ساده‌تر در `Serializers`
- برخی packageهای `Caching`
- برخی packageهای `DependencyInjection`

### دسته B: مهاجرت همراه با بهبود داخلی

این دسته شامل پروژه‌هایی است که علاوه بر migration، به مقداری تمیزکاری داخلی، اصلاح dependencyها یا build fix نیاز دارند، اما این اصلاحات breaking نیستند.

نمونه‌ها:
- برخی packageهای `Events`
- بعضی implementationهای `Caching`
- sample projectها
- بخشی از `Root Utilities`

### دسته C: مهاجرت همراه با deprecate

این دسته شامل پروژه‌ها یا APIهایی است که باید در نسخه 10 باقی بمانند، ولی از همین حالا برای حذف یا بازطراحی در نسخه 11 علامت‌گذاری می‌شوند.

نمونه‌ها:
- `Zamin.Extensions.Logger.Abstractions`
- بخش‌هایی از `MessageBus`
- بخش‌هایی از `Translations`
- بخش‌هایی از `Auth.ApiAuthentication`

## وضعیت packageهای abstraction

packageهای abstraction موج اول قبلاً به `.NET 10` مهاجرت داده شده‌اند و به‌عنوان پایه نسخه 10 framework در نظر گرفته می‌شوند.

این packageها شامل این موارد هستند:

- `Zamin.Extensions.Caching.Abstractions`
- `Zamin.Extensions.DependencyInjection.Abstractions`
- `Zamin.Extensions.Events.Abstractions`
- `Zamin.Extensions.ChangeDataLog.Abstractions`
- `Zamin.Extensions.ObjectMappers.Abstractions`
- `Zamin.Extensions.Serializers.Abstractions`
- `Zamin.Extensions.UsersManagement.Abstractions`
- `Zamin.Extensions.MessageBus.Abstractions`
- `Zamin.Extensions.Translations.Abstractions`

## وضعیت بخش‌های نیازمند بازبینی جدی

بخش‌های زیر همچنان نیازمند بازبینی جدی هستند، اما در نسخه 10 فقط migrate و در صورت لزوم deprecate می‌شوند:

### بخش مربوط به MessageBus
- پیچیدگی بالا
- سختی درک مدل consumer
- نیاز به ساده‌سازی اتصال به صف

### بخش مربوط به Translations
- مسئله culture handling
- وابستگی storage به پیاده‌سازی
- نیاز به بازبینی طراحی runtime

### بخش مربوط به Logger
- ابهام در اینکه abstraction واقعی هست یا نه
- احتمال حذف، ادغام یا بازطراحی

### بخش مربوط به Auth
- پیچیدگی بیشتر از حد انتظار
- فاصله گرفتن از نقش registration ساده

### بخش مربوط به SoftwarePartDetector
- ایده ارزشمند ولی نیازمند تثبیت API و مرزبندی دقیق‌تر

## راهبرد مستندسازی

در نسخه 10، مستندات باید هم‌زمان با migration تکمیل شوند.

تمرکز مستندات در این مرحله روی این بخش‌هاست:

- معرفی پروژه
- معماری فعلی
- ساختار build و packaging
- راهبرد migration
- deprecationها
- مستندات packageها
- تکمیل sampleها

## نتیجه مورد انتظار

در پایان نسخه 10 باید این وضعیت حاصل شده باشد:

- همه packageهای اصلی Zamin روی `.NET 10` باشند
- build و packaging به‌صورت یکدست و مرکزی مدیریت شود
- sampleهای اصلی قابل build باشند
- مستندات اصلی پروژه کامل و قابل اتکا باشند
- APIها و بخش‌های مسئله‌دار برای نسخه 11 به‌صورت رسمی deprecate شده باشند

</div>