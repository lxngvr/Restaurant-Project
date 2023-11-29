
SELECT FORMAT(tglfilter, 'dddd, dd MMMM yyyy ') AS "Tanggal Transaksi", 
       'Rp. ' + CONVERT(VARCHAR, CAST(SUM(totalBayar) AS MONEY), 1) AS "Pendapatan Harian"
FROM transaksi
GROUP BY tglfilter;

