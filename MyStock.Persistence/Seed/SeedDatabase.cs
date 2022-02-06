namespace MyStock.Persistence.Seed
{
    public class SeedDatabase
    {
        public static void Seed(IContext context)
        {
            SeedWarehouse(context);
            SeedUom(context);
            SeedProductCategory(context);
            context.SaveChanges();
        }

        static void SeedWarehouse(IContext context)
        {
            if (context.Set<Warehouse>().Any()) return;
            context.AddToContext(new Warehouse() { Name = "Главний", Description = "Склад по умолчанию" });
        }

        static void SeedUom(IContext context)
        {
            if (context.Set<Uom>().Any()) return;
            context.AddToContext(new Uom() { Name = "Штук", Code = "шт" });
            context.AddToContext(new Uom() { Name = "Бутылка", Code = "бтл" });
            context.AddToContext(new Uom() { Name = "Коробка", Code = "Коробка" });
            context.AddToContext(new Uom() { Name = "Ящик", Code = "Ящик" });
            context.AddToContext(new Uom() { Name = "Квадратный миллиметр", Code = "мм2" });
            context.AddToContext(new Uom() { Name = "Квадратный сантиметр", Code = "см2" });
            context.AddToContext(new Uom() { Name = "Квадратный метр", Code = "м2" });
            context.AddToContext(new Uom() { Name = "Сантиметр", Code = "см" });
            context.AddToContext(new Uom() { Name = "Дециметр", Code = "дм" });
            context.AddToContext(new Uom() { Name = "Километр", Code = "км" });
            context.AddToContext(new Uom() { Name = "Метр", Code = "м" });
            context.AddToContext(new Uom() { Name = "Миллиметр", Code = "мм" });
            context.AddToContext(new Uom() { Name = "Грамм", Code = "г" });
            context.AddToContext(new Uom() { Name = "Килограмм", Code = "кг" });
            context.AddToContext(new Uom() { Name = "Тонна", Code = "т" });
            context.AddToContext(new Uom() { Name = "Кубический сантиметр", Code = "см3" });
            context.AddToContext(new Uom() { Name = "Кубический дециметр", Code = "дм3" });
            context.AddToContext(new Uom() { Name = "Кубический метр", Code = "м3" });
            context.AddToContext(new Uom() { Name = "Кубический миллиметр", Code = "мм3" });
            context.AddToContext(new Uom() { Name = "Литр", Code = "л" });
            context.AddToContext(new Uom() { Name = "Миллилитр", Code = "мм" });
        }

        static void SeedProductCategory(IContext context)
        {
            if (context.Set<ProductCategory>().Any()) return;
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("195cd36c-28ce-4bf1-ad66-e8100fd75f7a"),
                Name = "мдф",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("2a4a7775-8de3-4aa4-804f-6b45024fef20"),
                Name = "абой",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("2cd3de67-005d-4584-a42b-ca66a32b504e"),
                Name = "10м",
                ParentGuid = Guid.Parse("2a4a7775-8de3-4aa4-804f-6b45024fef20")
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("35e1bbe7-4f78-46b6-a79d-7f636d073511"),
                Name = "Пластика",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("37ca6016-94d5-4933-8003-fe8e1bd9e071"),
                Name = "шпакиловка",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("40bfe345-30a1-47aa-b2f5-e5ff1fda4779"),
                Name = "1м",
                ParentGuid = Guid.Parse("2a4a7775-8de3-4aa4-804f-6b45024fef20")
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("47c1dd90-956f-450b-8061-1548b2da69e0"),
                Name = "10м кухни",
                ParentGuid = Guid.Parse("2a4a7775-8de3-4aa4-804f-6b45024fef20")
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("4846f838-78b5-418b-8a51-9fabeb78a2d0"),
                Name = "10м моюши",
                ParentGuid = Guid.Parse("2a4a7775-8de3-4aa4-804f-6b45024fef20")
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("4e5cfc6b-a7f5-40d4-944e-cf9a4b9effc0"),
                Name = "мех",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("526160dd-71ef-4026-9157-2d6f79db6dec"),
                Name = "водоемулсия",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("5d165655-d731-442c-9751-f837f6539af5"),
                Name = "бочка",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("5d2e1eeb-a1d2-4429-b8a9-98ab55f2db51"),
                Name = "вагонка",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("61cc755a-c7ff-4190-ba62-317e87e4db12"),
                Name = "1м моюши",
                ParentGuid = Guid.Parse("2a4a7775-8de3-4aa4-804f-6b45024fef20")
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("7fadc282-475b-4e2e-9b0d-d0d019243e28"),
                Name = "1м корея",
                ParentGuid = Guid.Parse("2a4a7775-8de3-4aa4-804f-6b45024fef20")
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("7febd40a-efa2-41fb-8c41-a032c8ff3305"),
                Name = "Лак ",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("80ce91cb-561c-4687-84db-579fef1e47b9"),
                Name = "15м",
                ParentGuid = Guid.Parse("2a4a7775-8de3-4aa4-804f-6b45024fef20")
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("89c3023a-176b-41ef-9c05-9c2a76bb983f"),
                Name = "река",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("9de4c36f-e06b-47ff-8f8b-d59509364863"),
                Name = "гипсокартон",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("9f424414-f5bc-497d-9d81-703886be19ad"),
                Name = "краска",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("a5cb5776-9d9c-4d13-b1d6-a1bfaa631c9d"),
                Name = "столичниса",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("a75f1846-cf64-40e8-8c0e-e3550eb83792"),
                Name = "наличник",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("abdfa5fe-617d-4e4a-be1a-aa11eef7c8a4"),
                Name = "10м бумажни",
                ParentGuid = Guid.Parse("2a4a7775-8de3-4aa4-804f-6b45024fef20")
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("ac7adcde-1bb2-4e0b-ac8d-7e329a5d0fb8"),
                Name = "дсп",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("c850e26a-ccb2-4522-842a-fd91379fb606"),
                Name = "75см моюши",
                ParentGuid = Guid.Parse("2a4a7775-8de3-4aa4-804f-6b45024fef20")
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("cc6bd67a-77d5-4a82-b1cf-c2818a38579a"),
                Name = "хархела",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("d5d42b57-94c0-4a6a-96af-cc9a21353eea"),
                Name = "тунука",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("d9ad5558-e4d0-4275-ac40-68c858f80a71"),
                Name = "25м сафед",
                ParentGuid = Guid.Parse("2a4a7775-8de3-4aa4-804f-6b45024fef20")
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("e1d60fda-cab0-4d5d-88ee-18350630fd82"),
                Name = "семент",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("e3d62479-03df-4501-a49f-083e02ed7d0a"),
                Name = "кафел",
                ParentGuid = null
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("e54d9f66-f8b0-4eb3-858c-ea2519df4559"),
                Name = "15м моюши",
                ParentGuid = Guid.Parse("2a4a7775-8de3-4aa4-804f-6b45024fef20")
            });
            context.AddToContext(new ProductCategory()
            {
                Guid = Guid.Parse("f2ce64d0-81e2-427e-bc3e-3c5722579b21"),
                Name = "дар",
                ParentGuid = null
            });
        }
    }
}
