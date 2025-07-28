# Zamin.Extensions.ObjectMappers.Mapster

پکیج توسعه داده شده برای استفاده از Mapster با قابلیت ثبت مپ‌های دلخواه به صورت داینامیک

## ویژگی‌ها

- تزریق `IObjectMapper` برای مپینگ داده‌ها بدون وابستگی مستقیم به Mapster
- ثبت مپ‌های سفارشی با `RegisterMap<TSource, TDestination>`
- اسکن خودکار اسمبلی‌ها برای کلاس‌های مپینگ استاندارد

## نحوه استفاده

```csharp
builder.Services.AddMapsterMappings(typeof(YourMappingClass).Assembly);

var mapper = serviceProvider.GetRequiredService<IObjectMapper>();

mapper.RegisterMap<Person, PersonDto>(cfg =>
{
    cfg.Map(dest => dest.FullName, src => $"{src.Name} {src.LastName}");
});

var dto = mapper.Map<PersonDto>(person);
