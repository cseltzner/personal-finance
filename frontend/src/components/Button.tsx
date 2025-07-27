type Props = React.ButtonHTMLAttributes<HTMLButtonElement> & {
    className?: string;
    isLoading?: boolean;
    loadingClassName?: string;
}

const Button = (props: Props) => {
  const { isLoading, children, ...rest } = props;
  return (
    <button
      {...rest}
      className={`bg-indigo-500 text-white px-4 py-2 rounded-sm hover:bg-indigo-600 transition-colors duration-100 cursor-pointer ${props.disabled ? "opacity-60 !cursor-not-allowed" : ""} ${props.className || ""}`}
      disabled={isLoading || rest.disabled}
    >
      {isLoading ? (
        <span className={`inline-block w-5 h-5 border-2 border-white border-t-transparent rounded-full animate-spin align-middle ${props.loadingClassName || ""}`}></span>
      ) : (
        children
      )}
    </button>
  );
};

export default Button;