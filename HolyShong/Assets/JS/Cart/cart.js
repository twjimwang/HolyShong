let cart = new Vue({
    el: '#cartApp',
    data: {
        product:
            {}
    },
    computed: {
        //sum(p) {
        //    console.log(p)
        //    let addAmount = 0;
        //    for (var i = 0; i < p.StoreProductOptions.length; i++) {
        //        for (var j = 0; j < p.StoreProductOptions[i].ProductOptionDetails.length; j++) {
        //            if (p.StoreProductOptions[i].ProductOptionDetails[j] == p.StoreProductOptions[i].SelectOption) {
        //                addAmount += p.StoreProductOptions[i].ProductOptionDetails[j].AddPrice;
        //            }
        //        }
        //    }
        //    return (p.UnitPrice + addAmount) * p.Quantity;
        //},
        totalSum() {
            let amount = this.product.map(p => p.Quantity * p.UnitPrice).reduce((a, b) => a + b);
            let addAmount = 0;
            for (var i = 0; i < this.product.length; i++) {
                for (var j = 0; j < this.product[i].StoreProductOptions.length; j++) {
                    for (var k = 0; k < this.product[i].StoreProductOptions[j].ProductOptionDetails.length; k++) {
                        if (this.product[i].StoreProductOptions[j].SelectOption == this.product[i].StoreProductOptions[j].ProductOptionDetails[k].StoreProductOptionDetailId) {
                            addAmount += this.product[i].StoreProductOptions[j].ProductOptionDetails[k].AddPrice
                        }
                    }
                    
                }
                
            }
            return amount + addAmount;
        }
    },
    methods: {
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
        },
        //checkOut() {
        //    $.ajax({
        //        type: 'POST',
        //        url: '/Cart/IsLogin',
        //        success: function (response) {
        //            if (response == 2) {
        //                $.ajax({
        //                    type: 'GET',
        //                    url: '/Member/CheckOut',
        //                    contentType: "application/json; charset=utf-8",
        //                    data: JSON.stringify(cart.product),
        //                    success: function (res) {
        //                        //沒有導向頁面
        //                        window.location.href = "/Member/checkout";
        //                    }
        //                });
        //            }
        //            else {
        //                window.location.href = "/Member/login";
        //            }
        //        }



        //    });
        //}
    }

    
});