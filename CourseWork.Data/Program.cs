using CourseWork.Lib.Entities;
using System;
using System.Collections.Generic;
using CourseWork.Lib;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CourseWork.Data.Scrapers;

namespace CourseWork.Data
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var db = new CWContext();
            db.Database.Migrate();

            db.SaveChanges();

            //await FillCourses();
            //await FillAreas();
        }

        static async Task FillCourses()
        {
            var udemy = new UdemyScraper();
            var courses = new List<Course>();

            await foreach (var slice in udemy.GetCoursesAsync())
            {
                courses.AddRange(slice);
            }

            courses = courses.GroupBy(c => c.ID).Select(g => g.First()).ToList();

            using var db = new CWContext();
            await db.Courses.AddRangeAsync(courses);
            await db.SaveChangesAsync();
        }

        static async Task FillAreas()
        {
            var hh = new HeadHunterScraper();
            var areas = await hh.GetAreasAsync();

            using var db = new CWContext();
            await db.Areas.AddRangeAsync(areas);
            await db.SaveChangesAsync();
        }
    }
}
