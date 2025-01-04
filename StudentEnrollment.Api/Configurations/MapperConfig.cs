using AutoMapper;
using StudentEnrollment.Api.DTOs.Course;
using StudentEnrollment.Api.DTOs.Enrollment;
using StudentEnrollment.Api.DTOs.Student;
using StudentEnrollment.Data;

namespace StudentEnrollment.Api.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Course, CreateCourseDto>().ReverseMap();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Course, CourseDetailsDto>()
            .ForMember(course => course.Students, option => option.MapFrom(course => course.Enrollments.Select(enrollment => enrollment.Student)));

        CreateMap<Student, CreateStudentDto>().ReverseMap();
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Student, StudentDetailsDto>()
            .ForMember(student => student.Courses, option => option.MapFrom(student => student.Enrollments.Select(enrollment => enrollment.Course)));

        CreateMap<Enrollment, CreateEnrollmentDto>().ReverseMap();
        CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
    }
}

