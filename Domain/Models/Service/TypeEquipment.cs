namespace Domain.Models.Service;

// TODO МОЖНО ЛИ СДЕЛАТЬ ОДНУ ТАБЛИЦУ->
// TODO КАТЕГОРИЯ РАБОТЫ = ТИП ОБОРУДОВАНИЯ -> ПОДПУНКТ 
public class TypeEquipment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ServiceId { get; set; }
}