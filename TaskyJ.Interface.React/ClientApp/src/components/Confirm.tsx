import * as React from "react";

interface IProps {
    title: string;
    content: string;
    cancelCaption?: string;
}

class Confirm extends React.Component<IProps> {
    public render() {
        return (
            <div>
                <div>
                    <div>
                        <span>{this.props.title}</span>
                    </div>
                    <div >
                        <p>{this.props.content}</p>
                    </div>
                    <div >
                        <button className="confirm-cancel" onClick={this.handleCancelClick}>Cancel</button>
                        <button className="confirm-ok" onClick={this.handleOkClick}>Okay</button>
                    </div>
                </div>
            </div>
        );
    }

    private handleOkClick = () => {
        console.log("Ok clicked", this.props);
    }

    private handleCancelClick = () => {
        console.log("Cancel clicked");
    }
}
export default Confirm;
