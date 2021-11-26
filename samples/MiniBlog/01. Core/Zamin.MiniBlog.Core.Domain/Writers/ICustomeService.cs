namespace Zamin.MiniBlog.Core.Domain.Writers;

public interface ICustomeServiceTransient
{
    void Exec();
}
public interface ICustomeServiceSingletone
{
    void Exec();
}
public interface ICustomeServiceScope
{
    void Exec();
}