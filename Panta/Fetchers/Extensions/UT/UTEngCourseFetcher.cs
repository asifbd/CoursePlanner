﻿using Panta.DataModels;
using Panta.DataModels.Extensions.UT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panta.Fetchers.Extensions.UT
{
    public class UTEngCourseFetcher : IItemFetcher<UTCourse>
    {
        private Dictionary<string, UTCourse> CoursesCollection { get; set; }

        public UTEngCourseFetcher()
        {
            this.CoursesCollection = new Dictionary<string, UTCourse>();
        }

        public IEnumerable<UTCourse> FetchItems()
        {
            IItemFetcher<UTCourse> fallCourseFetcher = new UTEngCourseInfoFetcher(@"http://www.apsc.utoronto.ca/timetable/fall.html");
            IItemFetcher<UTCourse> winterCourseFetcher = new UTEngCourseInfoFetcher(@"http://www.apsc.utoronto.ca/timetable/winter.html");
            IEnumerable<UTCourse> allCourses = fallCourseFetcher.FetchItems().Concat<UTCourse>(winterCourseFetcher.FetchItems());

            foreach (UTCourse course in allCourses)
            {
                course.Faculty = "Engineering";

                if (!CoursesCollection.ContainsKey(course.Abbr))
                {
                    CoursesCollection.Add(course.Abbr, course);
                }
                else
                {
                    //Console.WriteLine("Duplicate naming: " + course.Abbr);
                }
            }

            IEnumerable<UTCourse> coursesDetail = new UTEngCourseDetailFetcher(@"http://www.apsc.utoronto.ca/Calendars/2012-2013/Course_Descriptions.html").FetchItems();

            foreach (UTCourse detail in coursesDetail)
            {
                MergeCourses(this.CoursesCollection, detail);
            }

            return CoursesCollection.Values;
        }

        private bool MergeCourses(Dictionary<string, UTCourse> coursesCollection, UTCourse course)
        {
            UTCourse existedCourse;

            if (coursesCollection.TryGetValue(course.Abbr, out existedCourse))
            {
                existedCourse.Name = course.Name;
                existedCourse.Description = course.Description;
                existedCourse.Corequisites = course.Corequisites;
                existedCourse.Prerequisites = course.Prerequisites;
                existedCourse.Exclusions = course.Exclusions;
                existedCourse.DistributionRequirement = course.DistributionRequirement;
                existedCourse.Program = course.Program;
                return true;
            }
            return false;
        }
    }
}
