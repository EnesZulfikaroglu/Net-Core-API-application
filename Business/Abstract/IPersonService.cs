using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IPersonService
    {
        IDataResult<Person> GetById(int id);
        IDataResult<List<Person>> GetList();
        IDataResult<List<Person>> GetListByCity(string city);
        IResult Add(Person person);
        IResult Delete(Person person);
        IResult Update(Person person);
    }
}
