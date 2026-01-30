import express from "express";
import fetch from "node-fetch";

const app = express();
app.use(express.json());

// ===============================
// BANKA AYARLARI
// ===============================
const BANK_CONFIG = {
  baseUrl: "https://apigw.vakifbank.com.tr:8443",
  apiKey: "BANKADAN_ALINAN_API_KEY"
};

// ===============================
// YARDIMCI: TARİH FORMAT KONTROL
// ===============================
function isValidDate(dateStr) {
  return !isNaN(Date.parse(dateStr));
}

// ===============================
// ANA ENDPOINT
// ===============================
app.post("/transactions", async (req, res) => {

  // 🔹 DIŞARIDAN GELEN VERİ
  const { accountNumber, startDate, endDate } = req.body;

  // 🔹 VALIDATION
  if (!accountNumber || !startDate || !endDate) {
    return res.status(400).json({
      error: "accountNumber, startDate ve endDate zorunludur"
    });
  }

  if (!isValidDate(startDate) || !isValidDate(endDate)) {
    return res.status(400).json({
      error: "Tarih formatı geçersiz (yyyy-MM-dd)"
    });
  }

  // 🔹 BANKANIN İSTEDİĞİ FORMATA ÇEVİR
  const bankRequestBody = {
    AccountNumber: accountNumber,
    StartDate: `${startDate}T00:00:00`,
    EndDate: `${endDate}T23:59:59`
  };

  console.log("===== BANKAYA GIDEN ISTEK =====");
  console.log(JSON.stringify(bankRequestBody, null, 2));

  try {
    // 🔹 BANKAYA POST
    const response = await fetch(
      `${BANK_CONFIG.baseUrl}/accountTransactions`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "ApiKey": BANK_CONFIG.apiKey
        },
        body: JSON.stringify(bankRequestBody)
      }
    );

    const data = await response.json();

    console.log("===== BANKADAN GELEN RESPONSE =====");
    console.log(JSON.stringify(data, null, 2));

    // 🔹 BANKA HATA DÖNERSE
    if (data?.Header?.StatusCode !== "APIGW000000") {
      return res.status(400).json({
        bankError: data.Header
      });
    }

    // 🔹 SADECE HAREKETLERİ DÖN
    res.json({
      accountNumber,
      startDate,
      endDate,
      transactions: data.Data.AccountTransactions
    });

  } catch (err) {
    console.error("SISTEM HATASI:", err.message);
    res.status(500).json({
      error: "Banka servisine ulasilamadi"
    });
  }
});

// ===============================
// SERVER
// ===============================
app.listen(3000, () => {
  console.log("Server calisiyor: http://localhost:3000");
});
