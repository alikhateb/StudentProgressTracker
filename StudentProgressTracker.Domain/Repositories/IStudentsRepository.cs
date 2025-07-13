using StudentProgressTracker.Domain.Models;
using StudentProgressTracker.Shared.Repositories;

namespace StudentProgressTracker.Domain.Repositories;

public interface IStudentsRepository : IRepository<Student>;
