// ModalProvider.tsx
import { createContext, useContext, useState } from "react";
import type { ReactNode } from "react";
import { Modal, Box, IconButton, Typography } from "@mui/material";
import { Close } from "@mui/icons-material";

type ModalContent = ReactNode | null;

type ModalContextType = {
  openModal: (content: ReactNode, title?: string) => void;
  closeModal: () => void;
};

const ModalContext = createContext<ModalContextType | undefined>(undefined);

export function ModalProvider({ children }: { children: ReactNode }) {
  const [content, setContent] = useState<ModalContent>(null);
  const [title, setTitle] = useState<string | null>(null);

  const openModal = (newContent: ReactNode, title?: string) => {
    setContent(newContent);
    setTitle(title || null);
  };
  const closeModal = () => {
    setContent(null);
    setTitle(null);
  };

  return (
    <ModalContext.Provider value={{ openModal, closeModal }}>
      {children}

      <Modal open={!!content} onClose={closeModal}>
        <Box
          sx={{
            position: "absolute",
            top: "30%",
            left: "50%",
            transform: "translate(-50%, -50%)",
            bgcolor: "background.paper",
            boxShadow: 24,
            border: "2px solid #000",
            borderRadius: 2,
            pt: 6,
            pb: 8,
            px: 6,
            minWidth: 300,
            overflowY: "auto",
            display: "flex",
            flexDirection: "column",
          }}
        >
            <Box display="flex" justifyContent="space-between" alignItems="center" mb={3} gap={4}>
                <Typography variant="h6">
                    {title}
                </Typography>
                <IconButton
                    onClick={closeModal}>
                    <Close />
                </IconButton>
            </Box>
            {content}
        </Box>
      </Modal>
    </ModalContext.Provider>
  );
}

export function useModal() {
  const ctx = useContext(ModalContext);
  if (!ctx) throw new Error("useModal must be used within ModalProvider");
  return ctx;
}
