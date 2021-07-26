using CourseWork.Lib.Entities;
using System;

namespace CourseWork.Lib
{
    public interface ITrajectoryGenerator
    {
        Trajectory Generate(Specialization specialization, Guid userId, int size);
    }
}
