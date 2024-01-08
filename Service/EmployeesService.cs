using HRApp.Entities;
using HRApp.Repository;
namespace HRApp.service;
public class EmployeesService:IEmployeesService{
    //service is dependent on repository(Dao)
    private IEmployeesRepository empRepo;
    public EmployeesService(IEmployeesRepository repo)
    {
        empRepo=repo;
    }
    public List<Employee> getAll(){
        return empRepo.getAll();
    }
    public Employee getById(int id){
        return empRepo.getById(id);
    }

    public string insert(Employee emp){
       return empRepo.insert(emp);
    }

    public string update(Employee emp){
        return empRepo.update(emp);
    }
    public string delete(int id){
        return empRepo.delete(id);
    }
}