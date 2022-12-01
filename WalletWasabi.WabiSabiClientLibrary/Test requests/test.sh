echo "Get version"
./get-version.sh
echo
echo

echo "Analyze transaction"
./analyze-transaction.sh
echo
echo

echo "Create request"
./create-request.sh
echo
echo

echo "Create request for zero amount"
./create-request-for-zero-amount.sh
echo
echo

echo "Select utxo for round"
./select-utxo-for-round.sh
echo
echo

echo "Decompose amounts"
./decompose-amounts.sh
echo
echo

echo "Handle response"
./handle-response.sh
echo
echo
