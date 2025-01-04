namespace StudentEnrollment.Data.Contracts;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<Course> GetCourseDetails(int courseId);
}
