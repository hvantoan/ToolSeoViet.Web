using System.ComponentModel;

namespace TuanVu.Management.Web.Models {

    public enum ESetting {
        AutoGenerateProductCode = 101,
        AutoGenerateCustomerCode = 102,
        AutoGenerateSupplierCode = 103,
        AutoGenerateStoreCode = 104,
        AutoGenerateWarehouseCode = 105,
        AutoGenerateRoleCode = 106,
        AutoGenerateWarehouseImportCode = 107,
        AutoGenerateWarehouseExportCode = 108,
        AutoGenerateWarehouseTransferCode = 109,
        AutoGenerateWarehouseRefundCode = 110,
        AutoGenerateOrderNo = 111,
        AutoGenerateCategoryCode = 112,
        AutoGenerateWarehouseAdjustmentCode = 113,
    }

    public enum EWarehouseImport {
        Draft = 0,
        Completed = 5,
        Void = 9,
    }

    public enum EWarehouseExport {
        Draft = 0,
        Completed = 5,
        Void = 9,
    }

    public enum EWarehouseTransfer {
        Draft = 0,
        Sent = 1,
        Completed = 5,
        Void = 9,
    }

    public enum EWarehouseRefund {
        Draft = 0,
        Completed = 5,
        Void = 9,
    }

    public enum EWarehouseAdjustment {
        Draft = 0,
        Completed = 5,
        Void = 9,
    }

    public enum EDiscount {
        None = 0,
        Percent = 1,
        Value = 2,
    }

    public enum EDateTimePeriod {

        [Description("Ngày")]
        Day = 1,

        [Description("Tuần")]
        Week = 2,

        [Description("Tháng")]
        Month = 3,

        [Description("Năm")]
        Year = 4,
    }

    public enum EOrderStatus {

        [Description("Đơn tạm")]
        Draft = 0,

        [Description("Đơn mới")]
        New = 1,

        [Description("Xuất một phần")]
        Export = 4,

        [Description("Đã xuất")]
        Exported = 5,

        [Description("Đã huỷ")]
        Void = 9,
    }

    public enum EProductDocumentType {
        [Description("Nhập hàng")]
        Import = 1,

        [Description("Xuất hàng")]
        Export = 2,

        [Description("Chuyển hàng")]
        Transfer = 3,

        [Description("Trả hàng")]
        Refund = 4,

        [Description("Kiễm hàng")]
        Adjustment = 5,
    }

    public enum EImportStatus {
        None = 0,
        Warning = 1,
        Error = 2,
        Done = 3
    }
}