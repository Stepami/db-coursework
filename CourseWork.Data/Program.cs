using CourseWork.Lib.Entities;
using System;
using System.Collections.Generic;
using CourseWork.Lib;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourseWork.Data
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var db = new CWContext())
            {
                db.Database.Migrate();

                db.SaveChanges();
            }

            //await FillCourses();
        }

        static async Task FillCourses()
        {
            var udemy = new CoursesFromUdemy();
            var courses = new List<Course>();

            await foreach (var slice in udemy.GetCoursesAsync())
            {
                courses.AddRange(slice);
            }

            courses = courses.GroupBy(c => c.ID).Select(g => g.First()).ToList();

            using (var db = new CWContext())
            {
                await db.Courses.AddRangeAsync(courses);
                await db.SaveChangesAsync();
            }
        }
    }
}
