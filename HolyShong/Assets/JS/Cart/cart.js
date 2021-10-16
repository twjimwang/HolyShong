let cartApp = new Vue({
    el: '#cartApp',
    data: {
        haveCart : false,
        product: {
            StoreName:'',
            ProductId: 1,
            ProductName: '',
            ProductDescription: '',
            UnitPrice: 69.0,
            ProductImg: '',
            Quantity: 1,
            StoreProductOptions: [
                {
                    ProductOptionName: '',
                    ProductOptionDetails: [
                        {
                            StoreProductOptionDetailId: '',
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptionDetailId: '',
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptionDetailId: '',
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptionDetailId: '',
                            StoreProductOptioinDetailName: ''
                        }
                    ]
                }
        },
    },
    computed: {
    sum() {
        return this.product.UnitPrice * this.product.Quantity;
    }
}

    
});