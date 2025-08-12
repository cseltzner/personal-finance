import { Box, Select, TextField, Typography, MenuItem, type SelectChangeEvent, Button } from "@mui/material"
import type { Account } from "../../types/Account"
import { useState } from "react";
import { useModal } from "../../context/ModalContext";

type Props = {
    account?: Account;
}

const AccountModal = ({ account }: Props) => {
  const isEditMode = !!account;
  const [formState, setFormState] = useState<Partial<Account>>(account || {})
  const { closeModal } = useModal()

  const onFormChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement> | SelectChangeEvent) => {
    const { name, value } = e.target;
    setFormState((prev) => ({ ...prev, [name]: value }));
  }

  const onSave = () => {
    console.log("Saving account:", formState);
  }

  return (
      <Box display="flex" flexDirection="column" gap={3} minWidth="600px">
        <TextField
          id="AccountName"
          name="AccountName"
          label="Account Name"
          placeholder="Enter account name"
          type="text"
          value={formState.AccountName || ""}
          onChange={onFormChange}
          fullWidth
          margin="dense"
          variant="standard"
        />
        <TextField
          id="Description"
          name="Description"
          label="Description"
          placeholder="Enter account description"
          type="text"
          value={formState.Description || ""}
          onChange={onFormChange}
          fullWidth
          margin="dense"
          variant="standard"
        />
        <Select
            id="Type"
            name="Type"
            value={formState.Type || ""}
            onChange={onFormChange}
            defaultValue="checking"
            fullWidth
            variant="standard"
            displayEmpty
            sx={{ mt: 6, mb: 2}}
        >
            <MenuItem value="" disabled>Select Account Type</MenuItem>
            <MenuItem value="checking">Checking</MenuItem>
            <MenuItem value="savings">Savings</MenuItem>
            <MenuItem value="investment">Investment</MenuItem>
            <MenuItem value="retirement">Retirement</MenuItem>
            <MenuItem value="credit_card">Credit Card</MenuItem>
            <MenuItem value="cash">Cash</MenuItem>
            <MenuItem value="loan">Loan</MenuItem>
            <MenuItem value="other">Other</MenuItem>
        </Select>
        <TextField
          id="AccountNumber"
          name="AccountNumber"
          label="Account Number"
          placeholder="Enter account number"
          type="text"
          value={formState.AccountNumber || ""}
          onChange={onFormChange}
          fullWidth
          margin="dense"
          variant="standard"
      />
        <Box display="flex" justifyContent="flex-end" gap={4} mt={4}>
            <Button variant="outlined" color="primary" onClick={() => closeModal()}>
                Cancel
            </Button>
            <Button variant="contained" color="primary" onClick={onSave}>
                Save
            </Button>
        </Box>
        </Box>
  )
}

export default AccountModal