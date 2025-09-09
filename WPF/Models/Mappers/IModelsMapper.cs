using DOMAIN;
namespace WPF.Models.Mappers
{
    public interface IModelsMapper
    {
        List<Owner> map(List<OwnerDto> s);
        Owner map(OwnerDto s);
        Property map(PropertyDto s);
        Map_Point map(MapPointDto s);


        List<OwnerDto> map(List<Owner> s);
        OwnerDto map(Owner s);
        PropertyDto map(Property s);
        MapPointDto map(Map_Point s);
    }
}
