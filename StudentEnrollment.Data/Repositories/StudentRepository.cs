using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Data.Repositories;

public class StudentRepository(StudentEnrollmentDbContext context) : GenericRepository<Student>(context), IStudentRepository
{
    public async Task<Student> GetStudentDetails(int studentId)
    {
        return await context.Students
                     .Include(student => student.Enrollments)
                     .ThenInclude(enrollment => enrollment.Course)
                     .FirstOrDefaultAsync(student => student.Id == studentId);
    }
}
