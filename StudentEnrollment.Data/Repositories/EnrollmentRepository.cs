using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Data.Repositories;

public class EnrollmentRepository(StudentEnrollmentDbContext context) : GenericRepository<Enrollment>(context), IEnrollmentRepository;