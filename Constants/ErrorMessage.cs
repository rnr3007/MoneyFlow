namespace MoneyFlow.Constants
{
    public class ErrorMessage
    {
        public const string REQUIRED_DATA_EMPTY = "Data yang dibutuhkan tidak terisi";
        public const string USERNAME_EMPTY = "Username belum diisi";
        public const string PASSWORD_EMPTY = "Password belum diisi";
        public const string EMAIL_EMPTY = "Email belum diisi";
        public const string FULLNAME_EMPTY = "Nama lengkap belum diisi";
        public const string EXPENSE_NAME_EMPTY = "Nama pengeluaran belum diisi";
        public const string EXPENSE_COST_EMPTY = "Biaya pengeluaran belum diisi";
        public const string EXPENSE_TYPE_EMPTY = "Tipe pengeluaran belum diisi";
        public const string INCOME_TYPE_EMPTY = "Tipe pemasukan belum diisi"; 
        public const string TARGET_NAME_EMPTY = "Barang impian belum diisi";
        public const string TARGET_PRICE_EMPTY = "Harga barang impian belum diisi";
        public const string TARGET_IMAGE_EMPTY = "Gambar barang impian belum diisi";

        public const string FULLNAME_FORMAT_INVALID = "Format nama tidak sesuai";
        public const string USERNAME_FORMAT_INVALID = "Format username tidak sesuai";
        public const string PASSWORD_FORMAT_INVALID = "Format password tidak sesuai";
        public const string EMAIL_FORMAT_INVALID = "Format email tidak sesuai";
        public const string EXPENSE_COST_INVALID = "Nilai pengeluaran harus lebih dari 0";
        public const string EXPENSE_NAME_INVALID = "Maksimal nama pengeluaran 70 karakter";
        public const string EXPENSE_RECEIPT_INVALID = "Maksimal nama file 255 karakter";
        public const string FILE_SIZE_INVALID = "Ukuran file tidak boleh lebih dari 5MB";
        public const string FILE_TYPE_INVALID = "Tipe file tidak diperbolehkan";
        public const string TARGET_NAME_INVALID = "Nama barang impian terlalu panjang";
        public const string TARGET_PRICE_INVALID = "Harga barang impian tidak boleh kurang dari Rp1";

        public const string USER_NOT_FOUND = "User tidak ditemukan";
        public const string EXPENSE_NOT_FOUND = "Data pengeluaran tidak ditemukan";
        public const string INCOME_NOT_FOUND = "Data pendapatan tidak ditemukan";
        public const string MOTIVATION_NOT_FOUND = "Data barang impian belum diisi";
        
        public const string WRONG_PASSWORD = "Password salah";
        public const string FIELD_EMPTY = "Data kosong";
        public const string SERVER_ERROR = "Kesalahan server";
        public const string INVALID_TOKEN = "Token tidak valid";
    }
}