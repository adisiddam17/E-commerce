using HRApp.Entities;
namespace HRApp.service;

public interface IEmployeesService{
    public List<Employee> getAll();
    public Employee getById(int id);
    public string insert(Employee emp);
    public string update(Employee emp);
    public string delete(int id);
}