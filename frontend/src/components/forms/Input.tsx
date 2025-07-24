import type { InputHTMLAttributes } from "react";

type InputProps = InputHTMLAttributes<HTMLInputElement> & {
    label?: string;
    containerClassName?: string;
    labelClassName?: string;
    inputClassName?: string;
    errorClassName?: string;
    errorText?: string;
}

const Input = ({label, containerClassName, labelClassName, inputClassName, errorClassName, errorText, ...props}: InputProps) => {
  return (
    <div className={`flex flex-col gap-2 ${containerClassName || ""}`}>
     <label htmlFor={props.id} className={`mr-2 select-none text-sm font-semibold ${labelClassName || ""}`}>{label}</label>
      <input 
        {...props} 
        className={`border rounded-sm px-3 py-2 ${errorText ? "border-red-800" : "border-zinc-300"} focus:outline-none focus:ring-2 focus:ring-indigo-700 ${inputClassName || ""}`}
      />
        <span
        className={`text-sm min-h-[1.25rem] transition-all ${errorText ? "text-red-800" : "invisible"} ${errorClassName || ""}`}
        >
        {errorText || ""}
        </span>    
    </div>
  )
}

export default Input