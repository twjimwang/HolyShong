let cart = new Vue({
    el: '#cartApp',
    data: {
        product:
            [
                {
                    "StoreName": "MACU麻古茶坊",
                    "ProductId": 401,
                    "ProductName": "芝芝金萱",
                    "ProductDescription": "大。",
                    "UnitPrice": 60,
                    "ProductImg": "https://res.cloudinary.com/dvyxx4jau/image/upload/v1632415174/%E9%BA%BB%E5%8F%A4%E8%8C%B6%E6%88%BF/8820bcd5-6384-4e83-89e1-b45f862cfce6_p83adp.jpg",
                    "StoreProductOptions": [
                        {
                            "SelectOptionPrice": 0,
                            "SelectOption": "401",
                            "ProductOptionName": "糖量 Sweetness Level",
                            "ProductOptionDetails": [
                                {
                                    "StoreProductOptioinDetailName": "0分糖 Sugar-Free\r\n",
                                    "StoreProductOptionDetailId": 401,
                                    "AddPrice": 0
                                },
                                {
                                    "StoreProductOptioinDetailName": "1分糖 10% Sugar\r\n",
                                    "StoreProductOptionDetailId": 402,
                                    "AddPrice": 0
                                },
                                {
                                    "StoreProductOptioinDetailName": "3分糖 30% Sugar\r\n",
                                    "StoreProductOptionDetailId": 403,
                                    "AddPrice": 0
                                },
                                {
                                    "StoreProductOptioinDetailName": "5分糖 50% Sugar",
                                    "StoreProductOptionDetailId": 404,
                                    "AddPrice": 0
                                },
                                {
                                    "StoreProductOptioinDetailName": "8分糖 80% Sugar\r\n",
                                    "StoreProductOptionDetailId": 405,
                                    "AddPrice": 0
                                },
                                {
                                    "StoreProductOptioinDetailName": "全糖 Regular Sugar\r\n",
                                    "StoreProductOptionDetailId": 406,
                                    "AddPrice": 0
                                }
                            ]
                        }
                    ],
                    "Quantity": 1
                }
            ]
    },
    computed: {
        sum() {
            let amount = this.product.map(p => p.Quantity * p.UnitPrice).reduce((a, b) => a + b);
            return amount;
        }
    },
    methods: {
        removeItem(product, index) {
            if (product.Quantity == 0) {
                this.product.splice(index, 1);
                $.ajax
            }
        },
        closeCart() {
            $('#cart-check').click();
        },
        checkOut() {
            $.ajax({
                type: 'POST',
                url: '/Cart/IsLogin',
                success: function (response) {
                    if (response == 2) {
                        $.ajax({
                            type: 'POST',
                            url: '/Member/CheckOut',
                            data: JSON.stringify(this.product),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (res) {
                                console.log(res);
                            }
                        });
                    }
                    else {
                        window.location.href = "/Member/login";
                    }
                }



            });
        }
    }

    
});