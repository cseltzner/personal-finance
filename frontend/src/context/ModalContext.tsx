// ModalProvider.tsx
import { createContext, useContext, useState } from "react";
import type { ReactNode } from "react";
import { Modal, Box } from "@mui/material";

type ModalContent = ReactNode | null;

type ModalContextType = {
  openModal: (content: ReactNode) => void;
  closeModal: () => void;
};

const ModalContext = createContext<ModalContextType | undefined>(undefined);

export function ModalProvider({ children }: { children: ReactNode }) {
  const [content, setContent] = useState<ModalContent>(null);

  const openModal = (newContent: ReactNode) => setContent(newContent);
  const closeModal = () => setContent(null);

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
            border: "2px solid #000", // Not 100% sure if I want to keep this or not - Possibly remove later
            borderRadius: 2,
            p: 6,
            minWidth: 300,
          }}
        >
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
