using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Data.Repositories;

public class CourseRepository(StudentEnrollmentDbContext context) : GenericRepository<Course>(context), ICourseRepository
{
    public async Task<Course> GetCourseDetails(int courseId)
    {
        return await context.Courses
                     .Include(course => course.Enrollments)
                     .ThenInclude(enrollment => enrollment.Student)
                     .FirstOrDefaultAsync(course => course.Id == courseId);
    }
}
