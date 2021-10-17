console.log('12345')

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
                            "SelectOption": "401",
                            "ProductOptionName": "糖量 Sweetness Level",
                            "ProductOptionDetails": [
                                {
                                    "StoreProductOptioinDetailName": "0分糖 Sugar-Free\r\n",
                                    "StoreProductOptionDetailId": 401
                                },
                                {
                                    "StoreProductOptioinDetailName": "1分糖 10% Sugar\r\n",
                                    "StoreProductOptionDetailId": 402
                                },
                                {
                                    "StoreProductOptioinDetailName": "3分糖 30% Sugar\r\n",
                                    "StoreProductOptionDetailId": 403
                                },
                                {
                                    "StoreProductOptioinDetailName": "5分糖 50% Sugar",
                                    "StoreProductOptionDetailId": 404
                                },
                                {
                                    "StoreProductOptioinDetailName": "8分糖 80% Sugar\r\n",
                                    "StoreProductOptionDetailId": 405
                                },
                                {
                                    "StoreProductOptioinDetailName": "全糖 Regular Sugar\r\n",
                                    "StoreProductOptionDetailId": 406
                                }
                            ]
                        },
                        {
                            "SelectOption": null,
                            "ProductOptionName": "溫度 Temperature",
                            "ProductOptionDetails": [
                                {
                                    "StoreProductOptioinDetailName": "\n去冰 Ice-Free\r\n",
                                    "StoreProductOptionDetailId": 407
                                },
                                {
                                    "StoreProductOptioinDetailName": "微冰 Easy Ice\r\n",
                                    "StoreProductOptionDetailId": 408
                                },
                                {
                                    "StoreProductOptioinDetailName": "少冰 Less Ice\r\n",
                                    "StoreProductOptionDetailId": 409
                                },
                                {
                                    "StoreProductOptioinDetailName": "正常冰 Regular Ice\r\n",
                                    "StoreProductOptionDetailId": 410
                                },
                                {
                                    "StoreProductOptioinDetailName": "溫熱 Warm\r\n",
                                    "StoreProductOptionDetailId": 411
                                }
                            ]
                        },
                        {
                            "SelectOption": null,
                            "ProductOptionName": "加點 Add-Ons",
                            "ProductOptionDetails": [
                                {
                                    "StoreProductOptioinDetailName": "椰果 Coconut Jelly\r\n",
                                    "StoreProductOptionDetailId": 412
                                },
                                {
                                    "StoreProductOptioinDetailName": "珍珠 pearl",
                                    "StoreProductOptionDetailId": 413
                                },
                                {
                                    "StoreProductOptioinDetailName": "雙Q果 Tapioca Balls and Jelly",
                                    "StoreProductOptionDetailId": 414
                                },
                                {
                                    "StoreProductOptioinDetailName": "芝芝 Cheese",
                                    "StoreProductOptionDetailId": 415
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
            let amount = this.product.map(p => p.Quantity * p.UnitPrice).reduce((a,b)=> a+b);
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
        checkOut() {
            $.ajax({
                type: 'POST',
                url: '/Member/CheckOut',
                data: this.product,
                success: function (res) {
                    console.log(res);
                }
            });
        }
    }

    
});