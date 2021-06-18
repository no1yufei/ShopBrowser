using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data.Product
{
    public class LogisticsChannelsStatus
    {
        public LogisticsChannelStatus[] channel_status;
        public static LogisticsChannelsStatus FromJson(string str)
        {
            LogisticsChannelsStatus ret = new LogisticsChannelsStatus();
            try
            {
                ret = JsonConvert.DeserializeObject<LogisticsChannelsStatus>(str);
            }
            catch (Exception xe)
            {
                Console.WriteLine("LogisticsChannelStatus:" + xe.Message);
            }
            return ret;
        }
    }
    public class LogisticsChannelStatusV2
    {
        public string channel_id;//: "38011"
        public long shipping_fee;//: 7000000
        public int validation_status;//: 0
        public float weight;//: 0.45
    }
    public class LogisticsChannelStatus
    {
        public long channel_id;//: "38011"
        public string shipping_fee;//: 7000000
        public int validation_status;//: 0
        public string weight;//: 0.45
    }
    public class LogisticsChannels
    {
        public LogisticsChannelInfo[] logistics_channels;
       //     logistics-sizes: []
       public static LogisticsChannels FromJson(string str)
        {
            LogisticsChannels ret = new LogisticsChannels();
            try
            {

                ret = JsonConvert.DeserializeObject<LogisticsChannels>(str.Replace("-", "_"));
            }
            catch(Exception ex)
            {
                Console.WriteLine("LogisticsChannels:"+ex.Message);
            }
            return ret;
        }
    }

    public class ChannelsList
    {
        public ChannelInfo[] list;
        //     logistics-sizes: []
    }
    public class ChannelInfo
    {
        public long category_id;//: 10006
        public int channel_id;//: 38011
        public bool cod_enabled;//: true
        public bool cod_whitelist_enabled;//: true
                                          //conflicting_enabled_channels: []
        public int cover_shipping_fee;//: 0
public string default_price;//: "55.00"
//desc_key: "label_desc_yto_normal"
//discount: 0
//discount_json: ""
public string display_name;//: "蝦皮宅配"
        public bool enabled;//: true
        public bool enabled_mass_ship;//: true
        public string flag;//: "307388277500154897"
        public bool is_shipping_fee_promotion_rule;//: false
//limits: {item_min_weight: 0.001, list{}, dimension_sum: 0, checkout_max_weight: 20,…}
//channel_id: 38011
//checkout_max_weight: 20
//checkout_min_weight: 0
//dimension_sum: 0
//item_max_dimension: {}
//item_max_weight: 20
//item_min_weight: 0.001
//order_max_weight: 20
//order_min_weight: 0
//max_default_price: "1.00"
//max_height: 0
//max_size: 20
//min_default_price: "0.00"
public string name;//: "蝦皮宅配"
//name_key: "label_yto_normal"
//pickup_same_day_cutoff_hour: 0
//pre_print: false
//preferred: false
//priority: 4
//save_into_item: 0
public int size_id;//: 0
//sizes: []
//slugs: []
//transify_description_key: ""
//transify_name_key: ""
//volumetric_factor: 0
    }
    public class LogisticsChannelInfo
    {
        public long category;//: 10006
        public long channelid;//: 38011
        public int cod_enabled;//: 1
        public int cod_whitelist_enabled;//: 1
        public string command;//: ""
        public string country;//: "TW"
        public int cover_shipping_fee = 0;//: 0
                                      //public int default;//: 0
        public string desc_key;//: "label_desc_yto_normal"
        public double discount;//: 1.0
        public string discount_json;//: ""
        public string display_name;//: "蝦皮宅配"
        public int enable_massship;//: 0
        public int enabled;//: 0
        public string extra_data;//: "{"default_price": 5500000, "delivery_max_time": 10, "item_min_size": 0.001, "item_max_size": 20, "exclusive_channels": [38006], "delivery_min_time": 5, "consignment_expire_in_days": 90, "days_to_deliver": 15, "min_amount_need_ic": 120000000, "is_sls_asf": 1, "non_escrow_channel": 38006, "is_sls_shipping_fee": 1, "max_size": 20.0, "min_size": 0.0, "max_default_price": 100000, "decimal_places": 3, "max_rebate_amount": 4000000, "lane_code": "C-TW02", "shop_cod_whitelist_group_id": 701, "is_sls": 1, "default_size": 1.0, "min_order_total": 38800000, "migration_channels": [38006], "guarantee_extension_period": 3}"
        public string flag;//: "307388277500154897"
        public string icon;//: ""
        public string id;//: "default-38011"
        public bool is_shipping_fee_promotion_rule;//: true
        public string item_flag;//: "0"
        public int level;//: 2
        public LogisticsChannelLimitation limits;//: {item_min_weight: 0.001, checkout_max_weight: 20, order_max_weight: 20, order_min_weight: 0,…}
        public int mass_apply_prices;//: 0
        public string name;//: "蝦皮宅配"
        public string name_key;//: "label_yto_normal"
        public int preferred;//: 0
        public bool preprint;//: false
        public string gprice;//: "55.00"
        public int priority;//: 4
        public int save_into_item;//: 0
        public double size;//: 0.0
        public int sizeid;//: 0
                          //sizes: []
    }
    public class LogisticsChannelLimitation
    {
        public double checkout_max_weight;//;//: 10
        public double checkout_min_weight;//: 0
        public double item_max_weight;//: 5
        public double item_min_weight;//: 0.001
        public double order_max_weight;//: 10
        public double order_min_weight;//: 0
    }

}
