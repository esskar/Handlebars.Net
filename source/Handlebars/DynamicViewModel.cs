using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace HandlebarsDotNet
{
    public class DynamicViewModel : DynamicObject
    {
        public object[] Objects { get; set; }

        private const BindingFlags BindingFlags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase;

        public DynamicViewModel(params object[] objects)
        {
            Objects = objects;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return Objects.Select(o => o.GetType())
                .SelectMany(t => t.GetMembers(BindingFlags))
                .Select(m => m.Name);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            foreach (var target in Objects)
            {
                var member = target.GetType()
                    .GetMember(binder.Name, BindingFlags);

                if (member.Length > 0)
                {
                    if (member[0] is PropertyInfo propertyInfo)
                    {
                        result = propertyInfo.GetValue(target, null);
                        return true;
                    }
                    if (member[0] is FieldInfo fieldInfo)
                    {
                        result = fieldInfo.GetValue(target);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}