using NonFactors.Mvc.Grid.Web.Models;
using System.Linq;

namespace NonFactors.Mvc.Grid.Web.Processors
{
    public class ShortNameProcessor : IGridProcessor<PersonModel>
    {
        public GridProcessorType ProcessorType { get; set; }

        public ShortNameProcessor()
        {
            ProcessorType = GridProcessorType.Post;
        }

        public IQueryable<PersonModel> Process(IQueryable<PersonModel> items)
        {
            foreach (PersonModel person in items)
                person.Name = person.Name.Substring(0, 1) + ". " + person.Surname;

            return items;
        }
    }
}
