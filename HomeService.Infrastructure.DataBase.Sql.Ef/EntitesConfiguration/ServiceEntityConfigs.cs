using HomeService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.DataBase.Sql.Ef.EntitesConfiguration;

public class ServiceEntityConfigs : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");

        builder.HasKey(s => s.Id);

        builder
            .HasMany(s => s.Requests)
            .WithOne(r => r.Service)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasData(
            //اموزش
            new Service { Id = 1, CategoryId = 1, Title = "آموزش موسیقی ویولن", Price = 200000, ImgUrl="" },
            new Service { Id = 2, CategoryId = 1, Title = "آموزش تار", Price = 400000 },
            new Service { Id = 3, CategoryId = 1, Title = "آموزش هنگ درام", Price = 300000 },
            new Service { Id = 4, CategoryId = 1, Title = "آموزش زبان انگلیسی", Price = 500000 },
            new Service { Id = 5, CategoryId = 1, Title = "آموزش نقاشی و طراحی", Price = 700000 },
            new Service { Id = 6, CategoryId = 1, Title = "آموزش عکاسی", Price = 60000 },
            new Service { Id = 7, CategoryId = 1, Title = "آموزش آشپزی", Price = 55000 },
            new Service { Id = 8, CategoryId = 1, Title = "آموزش خیاطی", Price = 70000 },
            new Service { Id = 9, CategoryId = 1, Title = "آموزش پیانو", Price = 390000 },
            new Service { Id = 10, CategoryId = 1, Title = "آموزش رباتیک", Price = 450000 },
            //حمل و نقل
            new Service { Id = 11, CategoryId = 2, Title = "کارگر اسباب کشی", Price = 500000 },
            new Service { Id = 12, CategoryId = 2, Title = "سرویس و تعمیر خودرو", Price = 1000000 },
            new Service { Id = 13, CategoryId = 2, Title = "کارواش ماشین", Price = 100000 },
            new Service { Id = 14, CategoryId = 2, Title = "سرویس و تعمیر موتور سیکلت", Price = 350000 },
            new Service { Id = 15, CategoryId = 2, Title = "باربری و اتوبار", Price = 500000 },
            new Service { Id = 16, CategoryId = 2, Title = "صافکاری و نقاشی", Price = 2000000 },
            new Service { Id = 17, CategoryId = 2, Title = "اجار خودرو", Price = 1000000 },
            new Service { Id = 18, CategoryId = 2, Title = "بسته بندی و اسباب اثاثیه", Price = 600000 },
            //خدمات منزل
            new Service { Id = 19, CategoryId = 3, Title = "نظافت منزل", Price = 200000 },
            new Service { Id = 20, CategoryId = 3, Title = "نظافت راه پله و فضای مشاع", Price = 80000 },
            new Service { Id = 21, CategoryId = 3, Title = "نظافت سوله و انبار", Price = 80000 },
            new Service { Id = 22, CategoryId = 3, Title = "نظافت استخیر", Price = 100000 },
            new Service { Id = 23, CategoryId = 3, Title = "سرایداری و نگهبانی", Price = 500000 },
            new Service { Id = 24, CategoryId = 3, Title = "ضدعفونی منزل و محل کار", Price = 750000 },
            new Service { Id = 25, CategoryId = 3, Title = "قالیشویی", Price = 1000000 },
            new Service { Id = 26, CategoryId = 3, Title = "مبلشویی", Price = 1000000 },
            new Service { Id = 27, CategoryId = 3, Title = "فیشینگ ساختمان", Price = 800000 },
            //تاسیسات
            new Service { Id = 28, CategoryId = 4, Title = "نصب ماشین ظرفشویی", Price = 200000 },
            new Service { Id = 29, CategoryId = 4, Title = "نصب ماشین لباسشویی", Price = 200000 },
            new Service { Id = 30, CategoryId = 4, Title = "نصب و تعمیر شیرآلات", Price = 800000 },
            new Service { Id = 31, CategoryId = 4, Title = "نصب و تعمیر دوربین مداربسته", Price = 350000 },
            new Service { Id = 32, CategoryId = 4, Title = "نصب و تعمیر آیفون تصویری و صوتی", Price = 100000 },
            new Service { Id = 33, CategoryId = 4, Title = "سیم کشی ساختمان", Price = 3000000 },
            new Service { Id = 34, CategoryId = 4, Title = "نصب و تعمیر موتور برق", Price = 550000 },
            new Service { Id = 35, CategoryId = 4, Title = "نصب و تعمیر لوستر و برق", Price = 700000 },
            new Service { Id = 36, CategoryId = 4, Title = "نصب و تعمیر دزدگیر ساختمان", Price = 750000 },
            new Service { Id = 37, CategoryId = 4, Title = "تعمیر تلفن", Price = 200000 },
            new Service { Id = 38, CategoryId = 4, Title = "نصب و تعمیر دستگاه پوز و کارتخوان", Price = 200000 },
            new Service { Id = 39, CategoryId = 4, Title = "نصب و تعمیر پکیج سرمایشی و گرمایشی", Price = 1000000 },
            new Service { Id = 40, CategoryId = 4, Title = "خدمات برق صنعتی", Price = 1500000 },
            //ساخت و ساز و بنایی
            new Service { Id = 41, CategoryId = 5, Title = "ساخت و نصب و تعمیر سوله و کانکس", Price = 5000000 },
            new Service { Id = 42, CategoryId = 5, Title = "کارگر ساده", Price = 900000 },
            new Service { Id = 43, CategoryId = 5, Title = "بنایی", Price = 1000000 },
            new Service { Id = 44, CategoryId = 5, Title = "بازسازی خانه", Price = 10000000 },
            new Service { Id = 45, CategoryId = 5, Title = "سیمان کاری", Price = 5000000 },
            new Service { Id = 46, CategoryId = 5, Title = "نصب کاشی و سرامیک", Price = 550000 },
            new Service { Id = 47, CategoryId = 5, Title = "سنگ کاری", Price = 45000 },
            new Service { Id = 48, CategoryId = 5, Title = "گچ کاری و گچبری", Price = 20000000 },
            new Service { Id = 49, CategoryId = 5, Title = "ساخت و تعمیر استخر و سونا و جکوزی", Price = 30000000 },
            new Service { Id = 50, CategoryId = 5, Title = "تخریب ساختمان", Price = 20000000 },
            new Service { Id = 51, CategoryId = 5, Title = "رفع نم و رطوبت", Price = 1500000 },
            new Service { Id = 52, CategoryId = 5, Title = "رابیس کاری", Price = 1000000 },
            //خدمات زیبایی
            new Service { Id = 53, CategoryId = 6, Title = "رنگ مو", Price = 4000000 },
            new Service { Id = 54, CategoryId = 6, Title = "شینیون مو", Price = 1000000 },
            new Service { Id = 55, CategoryId = 6, Title = "تتو", Price = 3000000 },
            new Service { Id = 56, CategoryId = 6, Title = "اصلاح سر و صورت", Price = 300000 },
            new Service { Id = 57, CategoryId = 6, Title = "خدمات ناخن", Price = 500000 },
            new Service { Id = 58, CategoryId = 6, Title = "پاکسازی صورت", Price = 350000 },
            new Service { Id = 59, CategoryId = 6, Title = "میکاپ صورت", Price = 1500000 },
            new Service { Id = 60, CategoryId = 6, Title = "کراتینه مو", Price = 3000000 },
            new Service { Id = 61, CategoryId = 6, Title = "ویتامینه و ماسک مو", Price = 550000 },
            new Service { Id = 62, CategoryId = 6, Title = "میکرو بیلدینگ ابرو", Price = 3000000 },
            new Service { Id = 63, CategoryId = 6, Title = "لیفت مژه و آبرو", Price = 1000000 },
            //سلامت و بهداشت
            new Service { Id = 64, CategoryId = 7, Title = "تست و آزمایش", Price = 2500000 },
            new Service { Id = 65, CategoryId = 7, Title = "ویزیت پزشکی", Price = 200000 },
            new Service { Id = 66, CategoryId = 7, Title = "نوار قلب", Price = 500000 },
            new Service { Id = 67, CategoryId = 7, Title = "سونوگرافی و فیزیوتراپی", Price = 1000000 },
            //حیوانات
            new Service { Id = 68, CategoryId = 8, Title = "حمل و نقل حیوانات", Price = 200000 },
            new Service { Id = 69, CategoryId = 8, Title = "نگهداری حیوانات خانگی", Price = 200000 },
            new Service { Id = 70, CategoryId = 8, Title = "تربیت گربه", Price = 100000 },
            new Service { Id = 71, CategoryId = 8, Title = "تربیت سگ", Price = 100000 },
            new Service { Id = 72, CategoryId = 8, Title = "تربین اسب", Price = 200000 },
            new Service { Id = 73, CategoryId = 8, Title = "خدمات آزمایشگاهی حیوانات", Price = 200000 },
            new Service { Id = 74, CategoryId = 8, Title = "صدور شناسنامه حیوانات", Price = 350000 },
            new Service { Id = 75, CategoryId = 8, Title = "واکسن حیوانات", Price = 150000 },
            new Service { Id = 76, CategoryId = 8, Title = "معاینه و درمان حیوانات", Price = 100000 },
            new Service { Id = 77, CategoryId = 8, Title = "رادیولوژی و سونوگرافی حیوانات", Price = 400000 },
            //کسب و کار
            new Service { Id = 78, CategoryId = 9, Title = "مدلسازی کامپیوتری", Price = 100000 },
            new Service { Id = 79, CategoryId = 9, Title = "مشاوره مالیاتی", Price = 100000 },
            new Service { Id = 80, CategoryId = 9, Title = "مشاوره سرمایه گذاری", Price = 100000 },
            new Service { Id = 81, CategoryId = 9, Title = "مشاوره حقوقی", Price = 100000 },
            new Service { Id = 82, CategoryId = 9, Title = "طراحی کاتالوگ و بوروشور", Price = 100000 },
            new Service { Id = 83, CategoryId = 9, Title = "طراحی گرافیک وب", Price = 100000 },
            new Service { Id = 84, CategoryId = 9, Title = "طراحی Ui-Ux", Price = 100000 },
            new Service { Id = 85, CategoryId = 9, Title = "طراحی سابت", Price = 100000 },
            new Service { Id = 86, CategoryId = 9, Title = "ساخت اپلیکیشن", Price = 100000 },
            new Service { Id = 87, CategoryId = 9, Title = "تولید محتوای سایت", Price = 100000 },
            new Service { Id = 88, CategoryId = 9, Title = "زیرنویس فیلم", Price = 100000 },
            new Service { Id = 89, CategoryId = 9, Title = "طراحی پوستر", Price = 100000 },
            new Service { Id = 90, CategoryId = 9, Title = "طراحی لوگو", Price = 100000 },
            new Service { Id = 91, CategoryId = 9, Title = "قفسه بنوی فروشگاه و انبار", Price = 100000 },
            //دیجیتال
            new Service { Id = 92, CategoryId = 10, Title = "تعمیر کامپیووتر", Price = 350000 },
            new Service { Id = 93, CategoryId = 10, Title = "تعمیر لپ تاپ", Price = 350000 },
            new Service { Id = 94, CategoryId = 10, Title = "تعمیر دستگاه پرینتر", Price = 250000 },
            new Service { Id = 95, CategoryId = 10, Title = "سرویس و تعمیر برد هوشمند", Price = 500000 },
            new Service { Id = 96, CategoryId = 10, Title = "تعمیر ویندوز و نرم افزار", Price = 250000 },
            new Service { Id = 99, CategoryId = 10, Title = "مجازی سازی سرور", Price = 400000 },
            new Service { Id = 100, CategoryId = 10, Title = "خدمات امنیت شبکه", Price = 300000 },
            new Service { Id = 101, CategoryId = 10, Title = "تعمیرات موبایل و تبلت", Price = 1000000 },
            new Service { Id = 102, CategoryId = 10, Title = "تعمیر و نصب اسکنر", Price = 250000 },
            new Service { Id = 103, CategoryId = 10, Title = "سرویس و تعمیر دستگاه تست اسکناس", Price = 850000 },
            new Service { Id = 104, CategoryId = 10, Title = "سرویس و تعمیر دستگاه پول شمار", Price = 850000 },
            new Service { Id = 105, CategoryId = 10, Title = "سرویس و تعمیر دستگاه اثر اگشت", Price = 550000 }
            );
    }
}
