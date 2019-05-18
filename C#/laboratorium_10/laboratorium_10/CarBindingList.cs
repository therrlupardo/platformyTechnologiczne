using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace laboratorium_10
{
    class CarBindingList : BindingList<Car>
    {
        private ArrayList selectedIndices;
        private PropertyDescriptor sortPropertyValue;
        private ListSortDirection sortDirectionValue;
        private bool isSortedValue = false;

        public CarBindingList(List<Car> list)
        {
            foreach (var car in list)
            {
                base.Add(car);
            }
        }

        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }


        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortPropertyValue; }
        }

        protected override bool IsSortedCore
        {
            get { return isSortedValue; }
        }

        protected override ListSortDirection SortDirectionCore { get { return sortDirectionValue; } }


        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            var sortedList = new ArrayList();
            var unsortedList = new ArrayList(Count);
            if (prop.PropertyType.GetInterface("IComparable") != null)
            {
                sortPropertyValue = prop;
                sortDirectionValue = direction;

                foreach (Car car in Items)
                {
                    if (!sortedList.Contains(prop.GetValue(car)))
                    {
                        sortedList.Add(prop.GetValue(car));
                    }

                }
                sortedList.Sort();
                if (direction == ListSortDirection.Descending)
                {
                    sortedList.Reverse();
                }

                for (int i = 0; i < sortedList.Count; i++)
                {
                    var foundIndices = FindIndices(prop.Name, sortedList[i]);
                    foreach (var index in foundIndices)
                    {
                        unsortedList.Add(Items[index]);
                    }
                }
                isSortedValue = true;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
            else
            {
                throw new NotSupportedException($"Cannot sort by {prop.Name}. Lack of IComperable implementation.");
            }
        }

        public void Sort(string property, ListSortDirection direction)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Car));
            PropertyDescriptor prop = properties.Find(property, true);
            if (prop != null)
            {
                ApplySortCore(prop, direction);
            }
            else
            {
                throw new NotSupportedException($"Cannot sort by {prop.Name}, this property doesn\'t exist.");
            }
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            PropertyInfo propInfo = typeof(Car).GetProperty(prop.Name);
            selectedIndices = new ArrayList();
            int found = -1;

            if (key != null)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (propInfo.GetValue(Items[i], null).Equals(key))
                    {
                        found++;
                        selectedIndices.Add(i);
                    }
                }
            }
            return found;
        }

        public int[] FindIndices(string property, object key)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Car));
            PropertyDescriptor prop = properties.Find(property, true);
            if (prop != null)
            {
                if (FindCore(prop, key) >= 0)
                {
                    return (int[])(selectedIndices.ToArray(typeof(int)));
                }
            }
            return null;

        }

        public List<Car> FindCars(string property, object key)
        {

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Car));
            PropertyDescriptor prop = properties.Find(property, true);
            if (prop != null)
            {
                if (FindCore(prop, key) >= 0)
                {
                    List<Car> listOfMatchingCars = new List<Car>();
                    foreach (var index in selectedIndices)
                    {
                        listOfMatchingCars.Add(Items[(int)index]);
                    }
                    return listOfMatchingCars;
                }
            }
            return null;
        }

    }


}

