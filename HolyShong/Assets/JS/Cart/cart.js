let cart = new Vue({
    el: '#cartApp',
    data: {
        product:
            {}
    },
    computed: {
        totalSum() {
            let addAmount = 0;
            for (var i = 0; i < this.product.length; i++) {
                if (this.product[i].StoreProductOptions == null) { break;}
                for (var j = 0; j < this.product[i].StoreProductOptions.length; j++) {
                    for (var k = 0; k < this.product[i].StoreProductOptions[j].ProductOptionDetails.length; k++) {
                        if (this.product[i].StoreProductOptions[j].SelectOption == this.product[i].StoreProductOptions[j].ProductOptionDetails[k].StoreProductOptionDetailId) {
                            addAmount += this.product[i].StoreProductOptions[j].ProductOptionDetails[k].AddPrice;
                        }
                    }
                    
                }
            }
            let amount = this.product.map(p => p.Quantity * (p.UnitPrice + addAmount)).reduce((a, b) => a + b);
            return amount;
        }
    },
    methods: {
        sum(p) {
            console.log(p)
            let addAmount = 0;
            if (p.StoreProductOptions == null) { return p.UnitPrice * p.Quantity; }
            for (var i = 0; i < p.StoreProductOptions.length; i++) {
                for (var j = 0; j < p.StoreProductOptions[i].ProductOptionDetails.length; j++) {
                    if (p.StoreProductOptions[i].ProductOptionDetails[j].StoreProductOptionDetailId == p.StoreProductOptions[i].SelectOption) {
                        addAmount += p.StoreProductOptions[i].ProductOptionDetails[j].AddPrice;
                    }
                }
            }
            return (p.UnitPrice + addAmount) * p.Quantity;
        },
        removeItem(product, index) {
            if (product.Quantity == 0) {
                this.product.splice(index, 1);
                console.log(this.product)
                $.ajax({
                    type: 'POST',
                    url: '/api/Cart/DeleteCartItem',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(this.product),
                    success: function (response) {
                        console.log(response)
                        cart.product = response;
                    }

                });
            }
        },
        closeCart() {
            $('#cart-check').click();
        }
    }

    
});